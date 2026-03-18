using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class SchoolDegree
    {
        public int SchoolDegreeId { get; set; }
        public string SchoolDegreeName { get; set; } = string.Empty;
        public IEnumerable<Applicant>? Applicants { get; set; }
    }
}
