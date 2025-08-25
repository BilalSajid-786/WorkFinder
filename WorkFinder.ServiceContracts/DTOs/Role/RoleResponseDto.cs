using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Role
{
    public class RoleResponseDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
