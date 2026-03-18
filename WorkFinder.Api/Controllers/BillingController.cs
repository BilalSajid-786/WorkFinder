using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkFinder.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using WorkFinder.Api.Controllers.Base;
    using WorkFinder.ServiceContracts;

    [ApiController]
    [Route("api/billing")]
    public class BillingController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public BillingController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetBillingSummary()
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var stripeCustomerId = await _userService
                .GetUserStripeId(base.CurrentUser.BaseUserId);

            if (string.IsNullOrEmpty(stripeCustomerId))
                return Ok(null);

            var subscriptionService = new SubscriptionService();
            var invoiceService = new InvoiceService();
            var productService = new ProductService();

            // ✅ FIX: Only expand up to price (NOT product)
            var subscriptions = await subscriptionService.ListAsync(
                new SubscriptionListOptions
                {
                    Customer = stripeCustomerId,
                    Status = "all",
                    Expand = new List<string>
                    {
            "data.default_payment_method",
            "data.items.data.price"
                    }
                });

            var subscription = subscriptions.Data.FirstOrDefault();

            if (subscription == null)
                return Ok(null);

            // ✅ Fetch product separately
            var price = subscription.Items.Data.FirstOrDefault()?.Price;

            string planName = "Unknown Plan";

            if (price != null && !string.IsNullOrEmpty(price.ProductId))
            {
                var product = await productService.GetAsync(price.ProductId);
                planName = product?.Name ?? "Unknown Plan";
            }

            var invoices = await invoiceService.ListAsync(
                new InvoiceListOptions
                {
                    Customer = stripeCustomerId,
                    Limit = 10
                });

            var result = new
            {
                PlanName = planName,
                SubscriptionId = subscription.Id,
                Price = price?.UnitAmount != null
                    ? price.UnitAmount / 100.0m
                    : 0,

                //Currency = subscription.Currency,
                Status = subscription.Status,
                CurrentPeriodEnd = subscription.CurrentPeriodEnd,
                CancelAtPeriodEnd = subscription.CancelAtPeriodEnd,

                PaymentMethod = subscription.DefaultPaymentMethod is PaymentMethod pm
                    ? new
                    {
                        Brand = pm.Card?.Brand,
                        Last4 = pm.Card?.Last4,
                        ExpMonth = pm.Card?.ExpMonth,
                        ExpYear = pm.Card?.ExpYear
                    }
                    : null,

                Invoices = invoices.Data.Select(i => new
                {
                    AmountPaid = i.AmountPaid / 100.0m,
                    Status = i.Status,
                    Date = i.Created,
                    Pdf = i.InvoicePdf,
                    HostedUrl = i.HostedInvoiceUrl
                })
            };

            return Ok(result);
        }
    }
}
