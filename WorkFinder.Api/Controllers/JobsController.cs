using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Response;
using static WorkFinder.Entities.Entities.SystemSeeding.SystemPermissions;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : BaseApiController
    {
        private readonly IJobService _jobService;
        private readonly ResponseDto _responseDto;
        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
            _responseDto = new ResponseDto();
        }

        /// <summary>
        /// Inserts a job with given details
        /// </summary>
        /// <param name="jobRequestDto"></param>
        /// <returns>Inserted Job Details</returns>
        [Authorize(Policy = "Job.PostJob")]
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostJobAsync([FromBody] JobRequestDto jobRequestDto)
        {
            jobRequestDto.CreatedBy = CurrentUser.UserId;
            var jobResponse = await _jobService.InsertJobAsync(jobRequestDto);
            if(jobResponse.JobId != 0)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Job Post Success";
                _responseDto.Result = jobResponse;
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Job Post Failure";
            }
            return _responseDto;
        }
    }
}
