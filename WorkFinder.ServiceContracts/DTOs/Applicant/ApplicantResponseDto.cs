using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class ApplicantResponseDto
    {
        // From Applicant
        public Guid ApplicantId { get; set; }
        public Guid UserId { get; set; }
        public string? Resume { get; set; }
        public string Gender { get; set; } = string.Empty;         // applicant's gender
        public string ProfilePic { get; set; } = string.Empty;
        public int QualificationId { get; set; }
        public string Qualification { get; set; } = string.Empty;

        // From nested User (flattened for convenience)
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsActive { get; set; }                          // from User.IsActive

        // Collections (optional but handy)
        public List<SkillResponseDto> Skills { get; set; } = new();
    }
}
