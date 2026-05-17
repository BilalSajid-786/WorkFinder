using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.User
{
    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserEmail { get; set; }
        public Guid BaseUserId { get; set; }
        public string UserRole { get; set; } = string.Empty;
        public List<string>? Permissions { get; set; }
    }
}
