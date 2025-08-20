using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts.DTOs;

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
        /// Seed roles, if roles doesn't exist
        /// </summary>
        /// <returns></returns>
        public async Task SeedRolesAsync()
        {
            await _roleRepository.SeedRolesAsync();
        }
    }
}
