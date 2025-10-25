using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Messages;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Message
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user messages from system
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MessageResponseDto>> GetUserMessages(MessageRequestDto message)
        {
            var messages = await _messageRepository.GetUserMessages(_mapper.Map<Message>(message));
            return _mapper.Map<IEnumerable<MessageResponseDto>>(messages);
        }

        /// <summary>
        /// Insert messages into system
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task InsertMessage(MessageRequestDto message)
        {
            await _messageRepository.InsertMessage(_mapper.Map<Message>(message));
        }
    }
}
