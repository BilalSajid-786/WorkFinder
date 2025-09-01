using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Role
{
    public class RolePermissionResponseDto
    {
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
