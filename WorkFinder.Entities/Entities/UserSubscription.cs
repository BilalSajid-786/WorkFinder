using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    /// <summary>
    /// User Subscription Entity
    /// </summary>
    public class UserSubscription
    {
        public long Id { get; set; }  // Primary key, auto-increment

        public Guid UserId { get; set; }  // FK to Users.UserId

        public string StripeCustomerId { get; set; } = string.Empty;

        public string StripeSubscriptionId { get; set; } = string.Empty;

        public string? StripePaymentMethodId { get; set; }  // Nullable for trials

        public string SubscriptionStatus { get; set; } = string.Empty; // trialing, active, past_due, canceled
        public string InvoiceStatus { get; set; } = string.Empty;
        public string AccessStatus { get; set; } = string.Empty;
        public bool CancelAtPeriodEnd { get; set; }

        public DateTime? TrialStart { get; set; }

        public DateTime? TrialEnd { get; set; }

        public DateTime? CurrentPeriodStart { get; set; }

        public DateTime? CurrentPeriodEnd { get; set; }

        public decimal? Amount { get; set; }

        public string? Currency { get; set; }

        public string? PromoCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //Navigation Property to User
        public User? User { get; set; }
    }
}
