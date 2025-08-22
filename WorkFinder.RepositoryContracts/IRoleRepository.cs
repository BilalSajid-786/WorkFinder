using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Role Entity data operations
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Seeds roles into the database.
        /// </summary>
        /// <returns></returns>
        Task SeedRolesAsync();

        /// <summary>
        /// Gets all roles from the database
        /// </summary>
        /// <returns>All rows</returns>
        Task<IEnumerable<Role>> GetRolesAsync();
    }
}
