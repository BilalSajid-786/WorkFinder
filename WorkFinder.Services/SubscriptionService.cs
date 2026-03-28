using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Subscription;

namespace WorkFinder.Services
{
    /// <summary>
    /// Implementation for ISubscription Service
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IConfiguration _configuration;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public SubscriptionService(IConfiguration configuration, ISubscriptionRepository subscriptionRepository, IUserService userService,
            IEmailService emailService)
        {
            _configuration = configuration;
            _subscriptionRepository = subscriptionRepository;
            _userService = userService;
            _emailService = emailService;
        }
        /// <summary>
        /// Create Subscription for the given request. 
        /// This will handle the entire subscription creation process, including: trial, payment for application.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SubscriptionResponseDto> CreateSubscriptionAsync(CreateSubscriptionRequestDto request)
        {
            // 1️ Set Stripe secret key
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            // 2️ Create Stripe Customer
            var customerService = new CustomerService();
            var customer = await customerService.CreateAsync(new CustomerCreateOptions
            {
                Email = request.Email,
                PaymentMethod = request.PaymentMethodId,
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = request.PaymentMethodId
                },
                Metadata = new Dictionary<string, string>
                    {
                        { "UserId", request.UserId.ToString() },
                        { "PromoCode", request.PromoCode ?? "" }
                    }
            });

            // 3️ Check promo code
            bool giveTrial = request.PromoCode == "xyz";
            //bool giveTrial = true;

            // 4️ Prepare subscription options
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customer.Id,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = _configuration["Stripe:MonthlyPriceId"]
                    }
                },
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", request.UserId.ToString() },
                    { "PromoCode", request.PromoCode ?? "" }
                },
                Expand = new List<string> { "latest_invoice.payment_intent" } // Expand payment_intent
            };

            if (giveTrial)                 // Give trial: 6 months
                subscriptionOptions.TrialEnd = DateTime.UtcNow.AddMinutes(4);
            else                           // Charge immediately
                subscriptionOptions.PaymentBehavior = "default_incomplete";

            // 5️ Create subscription
            var subscriptionService = new Stripe.SubscriptionService();
            var subscription = await subscriptionService.CreateAsync(subscriptionOptions);

            // 6️ Get PaymentIntent if no trial
            PaymentIntent? paymentIntent = null;
            if (!giveTrial && subscription.LatestInvoice != null)
            {
                var latestInvoice = subscription.LatestInvoice as Invoice;
                paymentIntent = latestInvoice?.PaymentIntent;
            }

            //Persist subscription info in the db
            var invoiceStatus = "open"; // default when creating subscription
            var isTrial = giveTrial;
            var userSubscription = new UserSubscription
            {
                UserId = request.UserId,
                StripeCustomerId = customer.Id,
                StripeSubscriptionId = subscription.Id,
                StripePaymentMethodId = request.PaymentMethodId,
                SubscriptionStatus = subscription.Status,
                InvoiceStatus = invoiceStatus,
                AccessStatus = MapAccessStatus(subscription.Status, invoiceStatus, isTrial),
                TrialEnd = giveTrial ? DateTime.UtcNow.AddMinutes(4) : null,
            };

            await _subscriptionRepository.SaveUserSubscriptionInfo(userSubscription);

            return new SubscriptionResponseDto
            {
                SubscriptionId = subscription.Id,
                ClientSecret = paymentIntent?.ClientSecret,
                Status = subscription.Status,
                TrialGiven = giveTrial
            };
        }

        public async Task<SubscriptionResponseDto> CreateCheckoutSubscriptionAsync(CreateSubscriptionRequestDto request, string? customerId = null)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            bool giveTrial = request.PromoCode == "xyz";

            var subscriptionData = new SessionSubscriptionDataOptions
            {
                Metadata = new Dictionary<string, string>
                    {
                        { "UserId", request.UserId.ToString() },
                        { "PromoCode", request.PromoCode ?? "" }
                    },
            };

            // Add trial if applicable
            if (giveTrial)
            {
                subscriptionData.TrialEnd = DateTime.UtcNow.AddDays(3);
            }

            var sessionOptions = new SessionCreateOptions
            {
                Mode = "subscription",
                //CustomerEmail = request.Email,
                //Customer = "cus_U6W4Y4sbi3ZJUp",

                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = _configuration["Stripe:MonthlyPriceId"],
                    Quantity = 1
                }
            },

                SuccessUrl = "https://bilalsajid.xyz",
                CancelUrl = "https://yourdomain.com/cancel",

                Metadata = new Dictionary<string, string>
                {
                    { "UserId", request.UserId.ToString() },
                    { "PromoCode", request.PromoCode ?? "" }
                },
                 SubscriptionData = subscriptionData,
            };


            if (giveTrial)
            {
                sessionOptions.Metadata.Add("TrialEndDate", DateTime.UtcNow.AddDays(3).ToString());
            }

            if (customerId != null)
            {
                sessionOptions.Customer = customerId;
            }
            if (customerId == null)
            {
                sessionOptions.CustomerEmail = request.Email;
               // sessionOptions.Customer = "cus_U6cczJxv2oQVi8";
            }


            var service = new SessionService();
            var session = await service.CreateAsync(sessionOptions);

            return new SubscriptionResponseDto
            {
                SubscriptionId = null, // You will get this from webhook
                ClientSecret = null,
                Status = "checkout_created",
                TrialGiven = giveTrial,
                CheckoutUrl = session.Url
            };
        }


        /// <summary>
        /// Update Subscription By Id
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="cancelAtPeriodEnd"></param>
        /// <returns></returns>
        public async Task UpdateSubscriptionAsync(string subscriptionId, bool cancelAtPeriodEnd)
        {
            await _subscriptionRepository.UpdateSubscriptionById(subscriptionId, cancelAtPeriodEnd);
        }

        private string MapAccessStatus(string subscriptionStatus, string invoiceStatus, bool isTrial = false)
        {
            if (isTrial || subscriptionStatus == "trialing")
                return "allowed";

            return (subscriptionStatus, invoiceStatus) switch
            {
                ("active", "paid") => "allowed",
                ("active", "open") => "pending", // optional: allow limited features
                ("active", "past_due") => "pending",
                ("incomplete", _) => "denied",
                ("incomplete_expired", _) => "denied",
                ("canceled", _) => "denied",
                ("cancel_at_period_end", _) => "denied",
                _ => "denied"
            };
        }

        public async Task<string> GetOpenInvoicePaymentUrl(string customerId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
           
                var invoiceService = new InvoiceService();

                var invoices = await invoiceService.ListAsync(new InvoiceListOptions
                {
                    Customer = customerId,
                    Status = "open",
                    Limit = 1
                });

                var invoice = invoices.Data.FirstOrDefault();
                return invoice.HostedInvoiceUrl;
            
        }
    }
}
