using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Meeting;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : BaseApiController
    {
        private readonly IMeetingService _meetingService;
        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleMeeting([FromBody] MeetingRequestDto request)
        {
            var zoomMeeting = await _meetingService.CreateMeetingAsync(request.Topic, request.StartTime, request.Duration);
            return Ok(new{link = zoomMeeting });
        }
    }
}
