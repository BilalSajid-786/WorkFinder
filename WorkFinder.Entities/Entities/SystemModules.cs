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
        public static readonly Module Employer = new()
        {
            ModuleId = 1,
            ModuleName = "Employer"
        };
        public static readonly Module Applicant = new()
        {
            ModuleId = 2,
            ModuleName = "Applicant"
        };
        public static Module Job = new()
        {
            ModuleId = 3,
            ModuleName = "Job"
        };

        public static IEnumerable<Module> GetAllModules() => new List<Module>()
        { SystemModules.Employer, SystemModules.Applicant, SystemModules.Job };
    }
}
