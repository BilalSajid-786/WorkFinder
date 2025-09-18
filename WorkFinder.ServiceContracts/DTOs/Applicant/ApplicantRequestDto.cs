using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class ApplicantRequestDto : RegisterRequestDto
    {
        public string? Resume { get; set; }
        
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Qualification { get; set; } = string.Empty;

        public IEnumerable<SkillResponseDto> Skills { get; set; } = new List<SkillResponseDto>();
    }
}
