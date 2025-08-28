using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class ApplicantRequestDto : RegisterRequestDto
    {
        public string? ResumeUrl { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
    }
}
