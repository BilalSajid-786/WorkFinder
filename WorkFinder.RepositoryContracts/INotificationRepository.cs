using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Notification
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Insert notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task InsertNotification(Notification notification);

        /// <summary>
        /// Get notifications from system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotifications(Guid userId);

        /// <summary>
        /// Update Notification to mark it as read in system
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> UpdateNotification(int notificationId);
    }
}
