using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public class SystemDefaultRolePermissions
    {
        public static readonly Dictionary<Guid, List<Permission>> RolePermissions = new()
        {
            {
                Guid.Parse("E89D6301-F504-4C24-B934-6565906C6796"), //admin
                new List<Permission>
                {
                    SystemPermissions.Employer.Create,
                    SystemPermissions.Employer.Get,
                    SystemPermissions.Employer.Update,
                    SystemPermissions.Employer.Delete,
                    SystemPermissions.Applicant.Create,
                    SystemPermissions.Applicant.Get,
                    SystemPermissions.Applicant.Update,
                    SystemPermissions.Applicant.Delete,
                    SystemPermissions.Job.PostJob,
                    SystemPermissions.Job.InActiveJobs,
                    SystemPermissions.Job.ActiveJobs,
                    SystemPermissions.Dashboard.CanAccessDashboard
                }
            },
            {
                Guid.Parse("0A4B5DAA-8E42-46F1-B7AB-304806C6B996"), //employer
                new List<Permission>
                {
                    SystemPermissions.Job.PostJob,
                    SystemPermissions.Job.ActiveJobs,
                    SystemPermissions.Job.InActiveJobs,
                    SystemPermissions.Applicant.Get
                }
            },
            {
                Guid.Parse("4A1BDF6B-DF81-4E98-8DBE-F4E321CD82BA"), //applicant
                new List<Permission>
                {
                    SystemPermissions.Job.AppliedJobs,
                    SystemPermissions.Job.AvailableJobs,
                    SystemPermissions.Job.SavedJobs
                }
            }
        };
    }
}
