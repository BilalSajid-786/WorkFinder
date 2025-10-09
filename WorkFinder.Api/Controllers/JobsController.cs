using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Job;
using WorkFinder.ServiceContracts.DTOs.Pagination;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.Enums;
using WorkFinder.Services;
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
            try
            {
                jobRequestDto.CreatedBy = CurrentUser.BaseUserId;
                var jobResponse = await _jobService.InsertJobAsync(jobRequestDto);
                if (jobResponse.JobId != 0)
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
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Get available JobTypes
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Job.PostJob")]
        [HttpGet("jobTypes")]
        public ActionResult<ResponseDto> JobTypes()
        {
            try
            {
                _responseDto.Result = Enum.GetNames(typeof(JobType)).ToList();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize(Policy = "Job.AvailableJobs")]
        [HttpPost("availableJobs")]
        public async Task<ActionResult<ResponseDto>> GetApplicantAvailableJobs(ApplicantJobRequestDto applicantJobRequestDto)
        {
            try
            {
                _responseDto.Result = await _jobService.GetApplicantAvailableJobsAsync(applicantJobRequestDto);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        /// <summary>
        /// Get Active Jobs
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Job.ActiveJobs")]
        [HttpPost("activeJobs")]

        public async Task<ActionResult<ResponseDto>> GetActiveJobsAsyn(PaginationRequestDto paginationRequestDto)
        {
            try
            {
                Guid employerId = base.CurrentUser.UserId;
                var activeJobs = await _jobService.GetActiveJobsAsync(paginationRequestDto, employerId);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = activeJobs;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto; ;
        }
    }
}
