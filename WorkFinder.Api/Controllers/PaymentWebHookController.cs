using MailKit;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.IO;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentWebHookController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public PaymentWebHookController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        [HttpPost("callback")]
        public async Task<IActionResult> Callback()
        {
            // 1️⃣ Read the raw body
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            Event stripeEvent;

            try
            {
                // 2️⃣ Parse event without signature verification
                stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch: false);
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = $"Failed to parse event: {e.Message}" });
            }

            // 3️⃣ Handle specific event types
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    {
                        //var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        //Console.WriteLine($"PaymentIntent succeeded: {paymentIntent.Id}, Amount: {paymentIntent.Amount}");
                        //// TODO: Update your database / mark subscription as paid
                        //break;
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                        if (!string.IsNullOrEmpty(paymentIntent?.InvoiceId))
                        {
                            //var invoiceService = new InvoiceService();
                            var invoice = stripeEvent.Data.Object as Invoice;

                            var lineItem = invoice?.Lines.Data.FirstOrDefault();
                            string userId = lineItem?.Metadata["UserId"];

                            var subscriptionId = await _subscriptionRepository.GetSubscriptionIdByUserId(Guid.Parse(userId));
                            var subscriptionService = new SubscriptionService();
                            var subscription = await subscriptionService.GetAsync(subscriptionId);

                            if (!string.IsNullOrEmpty(userId))
                            {
                                await UpdateUserSubscriptionAsync(userId, subscription, invoice);
                            }
                        }
                        break;
                    }

                case Events.CheckoutSessionCompleted:
                    {
                        var session = stripeEvent.Data.Object as Session;

                        string? userId = session?.Metadata["UserId"];

                        DateTime? trialEndDate = null;

                        if (session?.Metadata?.TryGetValue("TrialEndDate", out var trialEndStr) == true)
                        {
                            trialEndDate = DateTime.Parse(trialEndStr);
                        }

                        var customerId = session?.CustomerId;
                        var subscriptionId = session?.SubscriptionId;

                        var subscriptionService = new SubscriptionService();
                        var subscription = await subscriptionService.GetAsync(subscriptionId);

                        var invoiceService = new InvoiceService();
                        var invoice = await invoiceService.GetAsync(subscription.LatestInvoiceId);

                        await _subscriptionRepository.SaveUserSubscriptionInfo(new UserSubscription
                        {
                            UserId = Guid.Parse(userId),
                            StripeCustomerId = customerId,
                            StripeSubscriptionId = subscriptionId,
                            SubscriptionStatus = subscription.Status,   // dynamic
                            InvoiceStatus = invoice.Status,              // dynamic
                            AccessStatus = MapAccessStatus(subscription.Status, invoice.Status),
                            TrialEnd = trialEndDate
                        });

                        break;
                    }


                case Events.InvoicePaid:
                    {
                        //with out checkout client secret code
                        var invoice = stripeEvent.Data.Object as Invoice;

                        var lineItem = invoice?.Lines.Data.FirstOrDefault();
                        string userId = lineItem?.Metadata["UserId"];

                        var invoiceService = new InvoiceService();

                        var invoiceExpaned = await invoiceService.GetAsync(invoice.Id, new InvoiceGetOptions
                        {
                            Expand = new List<string> { "subscription" }
                        });

                        //var subscriptionId = await _subscriptionRepository.GetSubscriptionIdByUserId(Guid.Parse(userId));
                        var subscriptionService = new SubscriptionService();
                        var subscription = await subscriptionService.GetAsync(invoiceExpaned.SubscriptionId);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            await UpdateUserSubscriptionAsync(userId, subscription, invoice);
                        }

                        break;
                    }

                case Events.InvoicePaymentFailed:
                    {
                        var invoice = stripeEvent.Data.Object as Invoice;

                        var lineItem = invoice?.Lines.Data.FirstOrDefault();
                        string userId = lineItem?.Metadata["UserId"];

                        var subscriptionId = await _subscriptionRepository.GetSubscriptionIdByUserId(Guid.Parse(userId));
                        var subscriptionService = new SubscriptionService();
                        var subscription = await subscriptionService.GetAsync(subscriptionId);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            await UpdateUserSubscriptionAsync(userId, subscription, invoice);
                        }
                        break;
                    }

                case Events.CustomerSubscriptionDeleted:
                    {
                        var subscription = stripeEvent.Data.Object as Subscription;

                        subscription.Metadata.TryGetValue("UserId", out var userId);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            await UpdateUserSubscriptionAsync(userId, subscription, null);
                        }

                        break;
                    }


                default:
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                    break;
            }

            // 4️⃣ Return 200 OK to Stripe
            return Ok();
        }

        private async Task UpdateUserSubscriptionAsync(string userId, Subscription subscription, Invoice invoice)
        {
            string subStatus = subscription?.Status ?? "unknown";
            string invStatus = invoice?.Status ?? "unknown";

            // Use custom mapper with trial flag
            string accessStatus = MapAccessStatus(subStatus, invStatus);

            Console.WriteLine($"User {userId}: Subscription={subStatus}, Invoice={invStatus}, Access={accessStatus}");

            await _subscriptionRepository.UpdateUserSubscriptionInfo(
                Guid.Parse(userId),
                subscription?.CustomerId,
                subStatus,
                invStatus,
                accessStatus,
                DateTime.UtcNow
            );
        }

        // Custom access mapper with trial support
        private string MapAccessStatus(string subscriptionStatus, string invoiceStatus)
        {
            if (subscriptionStatus?.ToLower() == "trialing")
                return "allowed";

            return (subscriptionStatus?.ToLower(), invoiceStatus?.ToLower()) switch
            {
                ("active", "paid") => "allowed",
                ("active", "open") => "pending",      // optional grace period
                ("active", "past_due") => "pending",
                ("incomplete", _) => "denied",
                ("incomplete_expired", _) => "denied",
                ("canceled", _) => "denied",
                ("cancel_at_period_end", _) => "denied",
                _ => "denied"
            };
        }
    }
}