using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Subscription
{
    public class SubscriptionResponseDto
    {
        public string SubscriptionId { get; set; } = string.Empty;
        public string? ClientSecret { get; set; }  // Present only if immediate payment
        public string Status { get; set; } = string.Empty;
        public bool TrialGiven { get; set; }
        public string CheckoutUrl { get; set; } = string.Empty; // Present only for checkout session creation
    }
}
