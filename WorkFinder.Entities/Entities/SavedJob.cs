using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class SavedJob
    {
        public Guid ApplicantId { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public Applicant? Applicant { get; set; }
    }
}
