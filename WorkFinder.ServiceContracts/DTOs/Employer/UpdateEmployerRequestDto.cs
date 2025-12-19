using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.ServiceContracts.DTOs.Employer
{
    public class UpdateEmployerRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string? Password { get; set; } = string.Empty;
        
        public Guid RoleId { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid EmployerId { get; set; }
        
        public string CompanyName { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        [Required]
        public int IndustryId { get; set; }
        [Required]
        public string CompanySize { get; set; } = string.Empty;
        
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }
    }
}
