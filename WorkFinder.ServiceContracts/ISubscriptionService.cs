using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Subscription;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Subscription Service for Users
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Create Subscription For the User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SubscriptionResponseDto> CreateSubscriptionAsync(CreateSubscriptionRequestDto request);

        /// <summary>
        /// Create Checkout Session for Subscription
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SubscriptionResponseDto> CreateCheckoutSubscriptionAsync(CreateSubscriptionRequestDto request, string? customerId = null, string? roleName = null);

        /// <summary>
        /// Update the subscription by Id
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        Task UpdateSubscriptionAsync(string subscriptionId, bool cancelAtPeriodEnd);

        /// <summary>
        /// Get open invoice for customer. This will be used to pay the invoice if subscription creation fails due to payment issues.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<string> GetOpenInvoicePaymentUrl(string customerId);
    }
}
