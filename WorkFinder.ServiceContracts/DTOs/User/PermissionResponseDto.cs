using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.User
{
    public class PermissionResponseDto
    {
        public int PermissionId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
