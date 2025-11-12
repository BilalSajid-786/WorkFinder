using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common.Dtos.Applicants
{
    public class ApplicantsFilter
    {
        public int? SkillId { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
