using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.Api.SignalR;
using WorkFinder.Common;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Messages;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{

    /// <summary>
    /// Messages controller to send messages to users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserConnectionManager _userConnectionManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly ResponseDto _responseDto;
        public MessagesController(IMessageService messageService, 
            IHubContext<ChatHub> hubContext,
            UserConnectionManager userConnectionManager,INotificationRepository notificationRepository)
        {
            _messageService = messageService;
            _hubContext = hubContext;
            _userConnectionManager = userConnectionManager;
            _notificationRepository = notificationRepository;
            _responseDto = new ResponseDto();
        }

        /// <summary>
        /// Get User Messages
        /// </summary>
        /// <param name="messageRequestDto"></param>
        /// <returns></returns>

        [HttpPost("getMessages")]
        public async Task<ResponseDto> GetUserMessages(MessageRequestDto messageRequestDto)
        {
            try
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = await _messageService.GetUserMessages(messageRequestDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Send message to a user
        /// </summary>
        /// <param name="messageRequestDto"></param>
        /// <returns></returns>
        [HttpPost("sendMessage")]
        public async Task<ResponseDto> SendMessage(MessageRequestDto messageRequestDto)
        {
            try
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";

                //Persist message in db for record
                var messageId = await _messageService.InsertMessage(messageRequestDto);
                
                //check if user is online on connection
                var connectionId = _userConnectionManager.GetConnectionId(messageRequestDto.ReceiverId);

                //if user is online send message via SignalR
                if(connectionId is not null)
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

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


    }
}
