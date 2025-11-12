using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.ServiceContracts.DTOs.Notifications;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Notifications
    /// </summary>
    public interface INotificationService
    {

        /// <summary>
        /// Get notifications from system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PaginatedList<NotificationResponseDto>> GetNotifications(Guid userId);

        /// <summary>
        /// Update notification to mark it as read in system
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        Task<PaginatedList<NotificationResponseDto>> UpdateNotification(int notificationId, Guid userId);
    }
}
