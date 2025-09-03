using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public static class SystemModules
    {
        public static readonly Module Manage = new()
        {
            ModuleId = 1,
            ModuleName = "Manage",
            Route = "/manage",
            ParentModuleId = null
        };
        public static readonly Module Employer = new()
        {
            ModuleId = 2,
            ModuleName = "Employer",
            Route = "/employer",
            ParentModuleId = 1
        };
        public static readonly Module Applicant = new()
        {
            ModuleId = 3,
            ModuleName = "Applicant",
            Route = "/applicant",
            ParentModuleId = 1
        };
        public static Module Job = new()
        {
            ModuleId = 4,
            ModuleName = "Job",
            Route = "/job",
            ParentModuleId = null
        };

        public static IEnumerable<Module> GetAllModules() => new List<Module>()
        { SystemModules.Manage,SystemModules.Employer, SystemModules.Applicant, SystemModules.Job };
    }
}
