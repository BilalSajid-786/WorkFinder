using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs
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
    }
}
