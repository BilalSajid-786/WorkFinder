using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Skill;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class JobResponseDto
    {
        public int JobId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public Guid EmployerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public JobType JobType { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsActive { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
        public List<SkillResponseDto> Skills { get; set; } = new();
    }
}
