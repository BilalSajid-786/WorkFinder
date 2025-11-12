using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Repository implementation for notification
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        private readonly DapperDbContext _dapperDbContext;

        public NotificationRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get notifications from the system for a given userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Notification>> GetNotifications(Guid userId)
        {
            var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetNotifications]";
            var paramaters = new DynamicParameters();
            paramaters.Add("@ReceiverId", userId);
            return await connection.QueryAsync<Notification>(sql, paramaters,
                commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert notification into db
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task InsertNotification(Notification notification)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertNotification]";
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId", notification.SenderId);
            parameters.Add("@SenderName", notification.SenderName);
            parameters.Add("@ReceiverId", notification.ReceiverId);
            parameters.Add("@MessageId", notification.MessageId);
            parameters.Add("@Content", notification.Content);
            parameters.Add("@CreatedAt", notification.CreatedAt);
            parameters.Add("@IsRead", notification.IsRead);
            await connection.ExecuteAsync(sql, parameters,commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Make notifications to read
        /// </summary>
        /// <param name="NotificationId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> UpdateNotification(int notificationId)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[UpdateNotification]";
            var parameters = new DynamicParameters();
            parameters.Add("@NotificationId", notificationId);
            return await connection.QueryAsync<Notification>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
