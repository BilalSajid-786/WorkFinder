using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
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
    }
}
