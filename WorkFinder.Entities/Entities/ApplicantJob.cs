using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class ApplicantJob
    {
        public Guid ApplicantId { get; set; }
        public int JobId { get; set; }
        public string Status { get; set; } = string.Empty;
        public Applicant? Applicant { get; set; }
        public Job? Job { get; set; }
    }
}
