using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;

namespace WorkFinder.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task SeedPermissionsAsync()
        {
            await _permissionRepository.SeedPermissionsAsync();
        }
    }
}
