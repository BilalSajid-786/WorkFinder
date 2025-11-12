using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Notifications;

namespace WorkFinder.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get notifications from system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PaginatedList<NotificationResponseDto>> GetNotifications(Guid userId)
        {
            var notifications = await _notificationRepository.GetNotifications(userId);
            var notificationsResponse =  _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
            int totalUnreadNotifications = notificationsResponse.Where(n => !n.IsRead).Count();
            return new PaginatedList<NotificationResponseDto>(notificationsResponse,totalUnreadNotifications,0,0);
        }

        /// <summary>
        /// Update notification to mark it as read in system
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public async Task<PaginatedList<NotificationResponseDto>> UpdateNotification(int notificationId, Guid userId)
        {
            await _notificationRepository.UpdateNotification(notificationId);
            return await GetNotifications(userId);
        }
    }
}
