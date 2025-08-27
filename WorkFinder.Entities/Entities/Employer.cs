using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Employer : User
    {
        public Guid EmployerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? CompanyWebsite { get; set; }
        public Guid IndustryId { get; set; }
        public string CompanySize { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }
    }
}
