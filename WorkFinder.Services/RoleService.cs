using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Role;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Role
    /// </summary>
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>Roles</returns>
        public async Task<IEnumerable<RoleResponseDto>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetRolesAsync();
            return roles.Select(r => new RoleResponseDto()
            {
                RoleId = r.RoleId,
                Name = r.RoleName,
            });
        }

        /// <summary>
        /// Seed roles, if roles doesn't exist
        /// </summary>
        /// <returns></returns>
        public async Task SeedRolesAsync()
        {
            await _roleRepository.SeedRolesAsync();
        }
    }
}
