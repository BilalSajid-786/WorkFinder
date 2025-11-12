using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common.Dtos.Jobs
{
    public class JobApplicantsFilter
    {
        public string? ApplicantStatus { get; set; }
        [Required]
        public int JobId { get; set; }
    }
}
