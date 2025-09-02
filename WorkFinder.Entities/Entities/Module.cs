using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string? Route { get; set; }
        public string? Icon { get; set; }
        IEnumerable<Permission>? Permissions { get; set; }
    }
}
