using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Modules
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Seed modules to the system
        /// </summary>
        /// <returns></returns>
        Task SeedModulesAsync();

        /// <summary>
        /// Get modules for user by role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>ParentModules and SubModules</returns>
        Task<IEnumerable<ParentModuleResponseDto>> GetSideBarItemsAsync(Guid roleId);
    }
}
