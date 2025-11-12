using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Notifications
{
    public class NotificationResponseDto
    {
        public Guid SenderId { get; set; }
        public int NotificationId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public static int TotalUnreadNotifications { get; set; }
    }
}
