using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common.Dtos.Jobs
{
    public class JobApplicantsFilter
    {
        public string? ApplicantStatus { get; set; }
        public int JobId { get; set; }
    }
}
