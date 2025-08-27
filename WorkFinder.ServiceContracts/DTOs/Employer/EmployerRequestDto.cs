using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Employer
{
    public class EmployerRequestDto
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string City {  get; set; } = string.Empty;
        [Required]
        public string Country {  get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        [Required]
        public Guid IndustryId { get; set; }
        [Required]
        public string CompanySize { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set;} = string.Empty;
        [Required]
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
