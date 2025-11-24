using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Common;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Messages;
using WorkFinder.Services.SignalR;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Message
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserConnectionManager _userConnectionManager;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository messageRepository, IMapper mapper, INotificationRepository notificationRepository,
            IHubContext<ChatHub> hubContext,UserConnectionManager userConnectionManager)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
            _userConnectionManager = userConnectionManager;
            _hubContext = hubContext;
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
        /// <param name="messageRequestDto"></param>
        /// <returns></returns>
        public async Task<int> InsertMessage(MessageRequestDto messageRequestDto)
        {
           var messageId = await _messageRepository.InsertMessage(_mapper.Map<Message>(messageRequestDto));
            //check if user is online on connection
            var connectionId = _userConnectionManager.GetConnectionId(messageRequestDto.ReceiverId);

            //if user is online send message via SignalR
            if (connectionId is not null)
            {
                await _hubContext.Clients.Client(connectionId)
                    .SendAsync("ReceiveMessage", messageRequestDto.SenderId, messageRequestDto.Text);
            }
            else
            {
                var notification = new Notification()
                {
                    SenderId = messageRequestDto.SenderId,
                    SenderName = messageRequestDto.SenderName,
                    ReceiverId = messageRequestDto.ReceiverId,
                    MessageId = messageId,
                    Content = $"New Message Received from {messageRequestDto.SenderName} on {DateTime.UtcNow}.\nClick here to view message."
                };
                await _notificationRepository.InsertNotification(notification);
            }
            return messageId;
        }
    }
}
