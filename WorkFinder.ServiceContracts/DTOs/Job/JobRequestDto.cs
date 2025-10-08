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

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class JobRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [EnumValidation(typeof(JobType))]
        public string JobType { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public Guid EmployerId { get; set; }
        [Required]
        public int IndustryId { get; set; }
        [JsonIgnore]
        public Guid CreatedBy { get; set; }
        public IEnumerable<SkillResponseDto> Skills { get; set; }
    }
}
