using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        public ModuleService(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParentModuleResponseDto>> GetSideBarItemsAsync(Guid roleId)
        {
            var modules = await _moduleRepository.GetSideBarItemsAsync(roleId);
            var items = MapSideBarItemByRoles(_mapper.Map<IEnumerable<ModuleResponseDto>>(modules));
            return items;
        }

        public async Task SeedModulesAsync()
        {
            await _moduleRepository.SeedModulesAsync();
        }

        private IEnumerable<ParentModuleResponseDto> MapSideBarItemByRoles(IEnumerable<ModuleResponseDto> modules)
        {
            // Modules having their parent modules existing
            var moduleWithParents = modules.Where(m => m.PermissionId == 0 && m.ParentModuleId == null)
                .Select(i => new ParentModuleResponseDto
                {
                    ParentModuleId = i.ModuleId,
                    ParentModuleName = i.ModuleName,
                    SubModules = modules.Where(s => s.ParentModuleId != null && s.ParentModuleId == i.ModuleId)
                    .GroupBy(s => s.ModuleId)
                    .Select(m => new ModuleResponseDto
                    {
                        ModuleId = m.Key,
                        ModuleName = m.First().ModuleName,
                        Route = m.First().Route,
                    }).ToList()
                });

            //Modules that don't have any parentModule
            var moduleWithOutParents = modules.Where(s => s.PermissionId != 0 && s.ParentModuleId == null)
                .GroupBy(s => s.ModuleId)
                .Select(g => new ParentModuleResponseDto
                {
                    ParentModuleId = g.Key,
                    ParentModuleName = g.First().ModuleName,
                    SubModules = g.Select(m => new ModuleResponseDto
                    {
                        ModuleId = g.Key,
                        ModuleName = m.DisplayName,
                    }).ToList(),
                });

            //Concat both modules
            return moduleWithParents.Concat(moduleWithOutParents);
        }

    }
}
