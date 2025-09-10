using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public static class SystemRoles
    {
        public static readonly Guid AdminId = Guid.Parse("E89D6301-F504-4C24-B934-6565906C6796");
        public static readonly Guid EmployerId = Guid.Parse("0A4B5DAA-8E42-46F1-B7AB-304806C6B996");
        public static readonly Guid ApplicantId = Guid.Parse("4A1BDF6B-DF81-4E98-8DBE-F4E321CD82BA");

        public const string Admin = "Admin";
        public const string Employer = "Employer";
        public const string Applicant = "Applicant";
    }
}
