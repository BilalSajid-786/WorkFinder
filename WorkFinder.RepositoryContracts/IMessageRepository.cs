using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Messages
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Insert message into db.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<int> InsertMessage(Message message);

        /// <summary>
        /// Get a list of all messages from db.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetUserMessages(Message message);
    }
}
