using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Attributes;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Skill;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class ApplicantRequestDto : RegisterRequestDto
    {
        public string? Resume { get; set; }
        
        [Required]
        [JsonIgnore]
        public Guid UserId { get; set; }

        [Required]
        public int QualificationId { get; set; }

        [Required]
        [EnumValidation(typeof(Gender))]
        public string Gender { get; set; } = string.Empty;

        public IEnumerable<SkillResponseDto> Skills { get; set; } = new List<SkillResponseDto>();
    }
}
