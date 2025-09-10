using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.User
{
    public class ModuleResponseDto
    {
        public int ModuleId { get; set; }
        public int? ParentModuleId { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int PermissionId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
    }
}
