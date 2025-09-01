using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    public interface IModuleService
    {
        /// <summary>
        /// Seed modules to the system
        /// </summary>
        /// <returns></returns>
        Task SeedModulesAsync();
    }
}
