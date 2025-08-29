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
        /// Seed the Permissions for every role, if permissions doesn't exist
        /// </summary>
        /// <returns></returns>
        Task SeedRolePermissionsAsync();

        /// <summary>
        /// Gets all roles from the database
        /// </summary>
        /// <returns>All rows</returns>
        Task<IEnumerable<Role>> GetRolesAsync();

        /// <summary>
        /// Get all permissions for every role from the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RolePermission>> GetRolePermissionsAsync();
    }
}
