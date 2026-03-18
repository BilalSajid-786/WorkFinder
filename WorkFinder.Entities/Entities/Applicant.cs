using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Applicant
    {
        public Guid ApplicantId { get; set; }
        public Guid UserId { get; set; }
        public string? Resume { get; set; }
        public string Gender { get; set; } = string.Empty;
        public User? User { get; set; }
        public IEnumerable<ApplicantSkill>? Skills { get; set; }
        public IEnumerable<ApplicantJob>? Jobs { get; set; }
        public IEnumerable<SavedJob>? SavedJobs { get; set; }
        public int QualificationId { get; set; }
        public Qualification? Qualification { get; set; }
        public int SchoolDegreeId { get; set; }
        public SchoolDegree? SchoolDegree { get; set; }

    }
}
