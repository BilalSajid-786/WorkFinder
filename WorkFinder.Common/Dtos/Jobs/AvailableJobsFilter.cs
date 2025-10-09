using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common.Dtos.Jobs
{
    public class AvailableJobsFilter
    {
        public string? Location { get; set; }
        public string? JobType { get; set; }
        public int? IndustryId { get; set; }
    }
}
