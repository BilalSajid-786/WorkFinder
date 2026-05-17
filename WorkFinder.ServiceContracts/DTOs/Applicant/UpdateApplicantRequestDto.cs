using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Attributes;
using WorkFinder.ServiceContracts.DTOs.Skill;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class UpdateApplicantRequestDto
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
        public string Country { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ApplicantId { get; set; }
        public string? Resume { get; set; }


        [Required]
        public int QualificationId { get; set; }

        [Required]
        [EnumValidation(typeof(Gender))]
        public string Gender { get; set; } = string.Empty;

        public IEnumerable<SkillResponseDto> Skills { get; set; } = new List<SkillResponseDto>();



    }
}
