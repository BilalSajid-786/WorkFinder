using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        private readonly ResponseDto _responseDto;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _responseDto = new ResponseDto();
        }

        /// <summary>
        /// Get notifications for logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetNotificationsAsync()
        {
            try
            {
                var notifications = await _notificationService.GetNotifications(CurrentUser.UserId);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = notifications;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("{notificationId}")]
        public async Task<ActionResult<ResponseDto>> UpdateNotification(int notificationId)
        {
            try
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = await _notificationService.UpdateNotification(notificationId,
                    CurrentUser.UserId);
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
