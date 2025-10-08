using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class ApplicantJobRequestDto
    {
        public string? JobType { get; set; }
        public string? Location { get; set; }
        public int? IndustryId { get; set; }
    }
}
