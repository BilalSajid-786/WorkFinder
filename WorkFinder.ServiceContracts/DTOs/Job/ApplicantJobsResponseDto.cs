using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class ApplicantJobsResponseDto
    {
        public int JobId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public Guid EmployerId { get; set; }
        public string? JobStatus { get; set; }
        public DateTime PostedDate { get; set; }
        public JobType JobType { get; set; }
        public IEnumerable<string>? Skills { get; set; }
    }
}
