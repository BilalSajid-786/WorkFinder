using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.User
{
    public class ParentModuleResponseDto
    {
        public int ParentModuleId { get; set; }
        public string ParentModuleName { get; set; } = string.Empty;
        public IEnumerable<ModuleResponseDto> SubModules { get; set; } = new List<ModuleResponseDto>();
    }
}
