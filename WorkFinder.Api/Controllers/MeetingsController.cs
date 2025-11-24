using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Meeting;
using WorkFinder.ServiceContracts.DTOs.Messages;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : BaseApiController
    {
        private readonly IMeetingService _meetingService;
        private readonly IMessageService _messageService;
        public MeetingsController(IMeetingService meetingService, IMessageService messageService)
        {
            _meetingService = meetingService;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleMeeting([FromBody] MeetingRequestDto request)
        {
            var zoomMeeting = await _meetingService.CreateMeetingAsync(request.Topic, request.StartTime, request.Duration);
            var messageRequestDto = new MessageRequestDto()
            {
                Text = zoomMeeting,
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId
            };
            await _messageService.InsertMessage(messageRequestDto);
            return Ok(new{link = zoomMeeting });
        }
    }
}
