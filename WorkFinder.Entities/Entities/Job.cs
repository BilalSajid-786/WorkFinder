using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Job : BaseEntity
    {
        public int JobId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public Guid EmployerId { get; set; }
        public int IndustryId { get; set; }
        public Employer? Employer { get; set; }
        public Industry? Industry { get; set; }
        public IEnumerable<JobSkill>? Skills { get; set; }
        public IEnumerable<ApplicantJobs>? Applicants { get; set; }
        public int TotalRows { get; set; }
    }
}
