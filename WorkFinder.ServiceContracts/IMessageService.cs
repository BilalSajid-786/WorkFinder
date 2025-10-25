using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Messages;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Service Contract for Messages
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Insert message into db.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task InsertMessage(MessageRequestDto message);

        /// <summary>
        /// Get a list of all messages from db.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<IEnumerable<MessageResponseDto>> GetUserMessages(MessageRequestDto message);
    }
}
