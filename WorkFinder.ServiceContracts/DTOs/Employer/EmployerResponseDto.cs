using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Employer
{
    public class EmployerResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid EmployerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public Guid IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive {get; set; }
        public string? WebsiteUrl { get; set; }
        public string CompanySize { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }

    }
}
