using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Subscription
{
    public class CreateSubscriptionRequestDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PaymentMethodId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? PromoCode { get; set; }
        public bool ChargeImmediately { get; set; }
    }
}
