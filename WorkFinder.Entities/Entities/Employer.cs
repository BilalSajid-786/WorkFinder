using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Employer
    {
        public Guid UserId { get; set; }
        public Guid EmployerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        public int IndustryId { get; set; }
        public Industry? Industry { get; set; }
        public string CompanySize { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }
        public User? User { get; set; }
    }
}
