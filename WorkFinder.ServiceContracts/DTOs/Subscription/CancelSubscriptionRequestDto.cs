using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Subscription
{
    public class CancelSubscriptionRequestDto
    {
        public string SubscriptionId { get; set; } = string.Empty;
    }
}
