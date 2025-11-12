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
    /// Repository implementation for messages
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public MessageRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get user messages from db
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetUserMessages(Message message)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetUserMessages]";
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId", message.SenderId);
            parameters.Add("@ReceiverId", message.ReceiverId);
            return await connection.QueryAsync<Message>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Insert a message into db
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<int> InsertMessage(Message message)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertMessage]";
            var parameters = new DynamicParameters();
            parameters.Add("@SenderId", message.SenderId);
            parameters.Add("@ReceiverId", message.ReceiverId);
            parameters.Add("@Text", message.Text);
            return await connection.ExecuteScalarAsync<int>(sql, parameters,commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
