using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public static class SystemPermissions
    {
        public static class Employer
        {
            public static readonly Permission Create = new() { PermissionId = 1, ModuleId = 2, Action = "Employer.Create" };
            public static readonly Permission Get = new() { PermissionId = 2, ModuleId = 2, Action = "Employer.Get" };
            public static readonly Permission Update = new() { PermissionId = 3, ModuleId = 2, Action = "Employer.Update" };
            public static readonly Permission Delete = new() { PermissionId = 4, ModuleId = 2, Action = "Employer.Delete" };
        }

        public static class Applicant
        {
            public static readonly Permission Create = new() { PermissionId = 5, ModuleId = 3, Action = "Applicant.Create" };
            public static readonly Permission Get = new() { PermissionId = 6, ModuleId = 3, Action = "Applicant.Get" };
            public static readonly Permission Update = new() { PermissionId = 7, ModuleId = 3, Action = "Applicant.Update" };
            public static readonly Permission Delete = new() { PermissionId = 8, ModuleId = 3, Action = "Applicant.Delete" };
        }

        public static class Job
        {
            public static readonly Permission PostJob = new() { PermissionId = 9, ModuleId = 4, Action = "Job.PostJob", DisplayName = "Post a Job" };
            public static readonly Permission ActiveJobs = new() { PermissionId = 10, ModuleId = 4, Action = "Job.ActiveJobs", DisplayName = "Active Jobs" };
            public static readonly Permission InActiveJobs = new() { PermissionId = 11, ModuleId = 4, Action = "Job.InActiveJobs", DisplayName = "Inactive Jobs" };
            public static readonly Permission AvailableJobs = new() { PermissionId = 12, ModuleId = 4, Action = "Job.AvailableJobs", DisplayName = "Available Jobs", Route = "/availablejobs" };
            public static readonly Permission AppliedJobs = new() { PermissionId = 13, ModuleId = 4, Action = "Job.AppliedJobs", DisplayName = "Applied Jobs", Route = "/appliedjobs" };
            public static readonly Permission SavedJobs = new() { PermissionId = 14, ModuleId = 4, Action = "Job.SavedJobs", DisplayName = "Saved Jobs", Route = "/savedjobs" };
        }

        public static class Dashboard
        {
            public static readonly Permission CanAccessDashboard = new() { PermissionId = 15, Action = "Dashboard.CanAccessDashboard" };
        }

        public static IEnumerable<Permission> GetAllPermissions() =>
                                new List<Permission>() { Employer.Create,Employer.Get,Employer.Update,Employer.Delete,
                                Applicant.Create,Applicant.Get,Applicant.Update,Applicant.Delete,
                                Job.PostJob,Job.ActiveJobs,Job.InActiveJobs,Job.AvailableJobs,Job.AppliedJobs,Job.SavedJobs,Dashboard.CanAccessDashboard};
    }
}
