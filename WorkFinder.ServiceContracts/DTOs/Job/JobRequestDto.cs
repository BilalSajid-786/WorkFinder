using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Job
{
    public class JobRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public Guid EmployerId { get; set; }
        public int IndustryId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
