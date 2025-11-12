using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Attributes;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class UpdateJobApplicantStatusRequestDto
    {
        [Required]
        public Guid ApplicantId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        [EnumValidation(typeof(StatusType))]
        public string ApplicantStatus { get; set; } = string.Empty;

    }
}
