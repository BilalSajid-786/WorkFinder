using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public int? ModuleId { get; set; }
        public Module? Module { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? DisplayName { get; set; } = string.Empty;
        public IEnumerable<RolePermission>? Roles { get; set; }
    }
}
