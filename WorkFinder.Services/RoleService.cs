using AutoMapper;
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
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get role permissions for given roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RolePermissionResponseDto>> GetRolePermissionsByRoleIdAsync(Guid roleId)
        {
            var permissions = await _roleRepository.GetRolePermissionsByRoleIdAsync(roleId);
            return _mapper.Map<IEnumerable<RolePermissionResponseDto>>(permissions);
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
        /// Seed role permissions, if permissions doesn't exist
        /// </summary>
        /// <returns></returns>
        public async Task SeedRolePermissionsAsync()
        {
            await _roleRepository.SeedRolePermissionsAsync();
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
