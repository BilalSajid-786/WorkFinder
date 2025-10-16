using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class ApplicantApplyJobDto
    {
        [JsonIgnore]
        public Guid ApplicantId { get; set; }
        [Required]
        public int JobId { get; set; }
    }
}
