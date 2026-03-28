using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.User
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public Guid BaseUserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ProfilePic { get; set; } = "NoImage.png";
        public string SubscriptionStatus { get; set; } = string.Empty;
        public string AccessStatus { get; set; } = string.Empty;
        public string StripeCustomerId { get; set; } = string.Empty;
        public string? Country { get; set; }


        [JsonIgnore]
        public string? CompanyName { get; set; } = string.Empty;
    }
}
