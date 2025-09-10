using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contracts for Modules
    /// </summary>
    public interface IModuleRepository
    {
        /// <summary>
        /// Seed modules to the database
        /// </summary>
        /// <returns></returns>
        Task SeedModulesAsync();

        /// <summary>
        /// Get all modules from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Module>> GetAllModulesAsync();

        /// <summary>
        /// Get modules for user by role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Modules</returns>
        Task<IEnumerable<Permission>> GetSideBarItemsAsync(Guid roleId);
    }
}
