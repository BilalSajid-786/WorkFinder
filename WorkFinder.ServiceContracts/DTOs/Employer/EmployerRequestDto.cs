using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.ServiceContracts.DTOs.Employer
{
    public class EmployerRequestDto : RegisterRequestDto
    {
        public Guid? UserId { get; set; }
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        [Required]
        public int IndustryId { get; set; }
        [Required]
        public string CompanySize { get; set; } = string.Empty;
        [Required]
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }
    }
}
