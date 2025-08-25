using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Role;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Role
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Seed the roles, if roles doesn't exist
        /// </summary>
        /// <returns></returns>
        Task SeedRolesAsync();

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>Roles</returns>
        Task<IEnumerable<RoleResponseDto>> GetRolesAsync();
    }
}
