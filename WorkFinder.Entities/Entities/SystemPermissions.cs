using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public static class SystemPermissions
    {
        public static class Employer
        {
            public static readonly Permission Create = new() {PermissionId = 1, ModuleId = 1, Action = "Employer.Create" };
            public static readonly Permission Get = new() {PermissionId = 2, ModuleId = 1, Action = "Employer.Get" };
            public static readonly Permission Update = new() {PermissionId = 3, ModuleId = 1, Action = "Employer.Update" };
            public static readonly Permission Delete = new() {PermissionId = 4, ModuleId = 1, Action = "Employer.Delete" };
        }

        public static class Applicant
        {
            public static readonly Permission Create = new() { PermissionId = 5, ModuleId = 2, Action = "Applicant.Create" };
            public static readonly Permission Get = new() { PermissionId = 6, ModuleId = 2, Action = "Applicant.Get" };
            public static readonly Permission Update = new() { PermissionId = 7, ModuleId = 2, Action = "Applicant.Update" };
            public static readonly Permission Delete = new() { PermissionId = 8, ModuleId = 2, Action = "Applicant.Delete" };
        }

        public static class Job
        {
            public static readonly Permission Create = new() { PermissionId = 9, ModuleId = 3, Action = "Job.Create" };
            public static readonly Permission ApplyJob = new() { PermissionId = 10, ModuleId = 3, Action = "Job.Apply" };
            public static readonly Permission ViewCreated = new() { PermissionId = 11, ModuleId = 3, Action = "Job.ViewCreated" };
            public static readonly Permission ViewRelevant = new() { PermissionId = 12, ModuleId = 3, Action = "Job.ViewRelevant" };
        }

        public static IEnumerable<Permission> GetAllPermissions() =>
                                new List<Permission>() { Employer.Create,Employer.Get,Employer.Update,Employer.Delete,
                                Applicant.Create,Applicant.Get,Applicant.Update,Applicant.Delete,
                                Job.Create,Job.ApplyJob,Job.ViewCreated,Job.ViewRelevant};
    }
}
