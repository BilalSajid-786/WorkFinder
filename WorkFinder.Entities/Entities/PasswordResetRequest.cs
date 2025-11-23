using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class PasswordResetRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
        public User? User { get; set; }
    }
}
