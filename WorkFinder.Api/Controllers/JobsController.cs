using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
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

        /// <summary>
        /// Get Available Jobs for an applicant
        /// </summary>
        /// <param name="applicantJobRequestDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "Job.AvailableJobs")]
        [HttpPost("availableJobs")]
        public async Task<ActionResult<ResponseDto>> GetApplicantAvailableJobs(PaginationParameters<AvailableJobsFilter> applicantJobRequestDto)
        {
            try
            {
                if (applicantJobRequestDto.Filters != null)
                    applicantJobRequestDto.Filters.ApplicantId = base.CurrentUser.UserId;
                else
                    applicantJobRequestDto.Filters = new()
                    {
                        ApplicantId = base.CurrentUser.UserId,
                    };
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
        /// Get Available Jobs for an applicant
        /// </summary>
        /// <param name="applicantJobRequestDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "Job.AppliedJobs")]
        [HttpPost("appliedJobs")]
        public async Task<ActionResult<ResponseDto>> GetApplicantAppliedJobs(PaginationParameters<AvailableJobsFilter> applicantJobRequestDto)
        {
            try
            {
                applicantJobRequestDto.Filters = new()
                {
                    ApplicantId = base.CurrentUser.UserId,
                };
                _responseDto.Result = await _jobService.GetApplicantAppliedJobsAsync(applicantJobRequestDto);
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
        /// Get Saved Jobs for an applicant
        /// </summary>
        /// <param name="applicantJobRequestDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "Job.SavedJobs")]
        [HttpPost("savedJobs")]
        public async Task<ActionResult<ResponseDto>> GetApplicantSavedJobs(PaginationParameters<AvailableJobsFilter> applicantJobRequestDto)
        {
            try
            {
                applicantJobRequestDto.Filters = new()
                {
                    ApplicantId = base.CurrentUser.UserId,
                };
                _responseDto.Result = await _jobService.GetApplicantSavedJobsAsync(applicantJobRequestDto);
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
        /// Apply job for an applicant
        /// </summary>
        /// <param name="applicantApplyJobDto"></param>
        /// <returns>true or false</returns>
        [Authorize(Policy = "Job.AppliedJobs")]
        [HttpPost("applyJob")]
        public async Task<ActionResult<ResponseDto>> ApplyJob(ApplicantApplyJobDto applicantApplyJobDto)
        {
            try
            {
                applicantApplyJobDto.ApplicantId = base.CurrentUser.UserId;
                _responseDto.Result = await _jobService.ApplyJobAsync(applicantApplyJobDto);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Job Application Successfull";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Save job for an applicant
        /// </summary>
        /// <param name="applicantSaveJobDto"></param>
        /// <returns>true or false</returns>
        [Authorize(Policy = "Job.SavedJobs")]
        [HttpPost("saveJob")]
        public async Task<ActionResult<ResponseDto>> SaveJob(ApplicantApplyJobDto applicantSaveJobDto)
        {
            try
            {
                applicantSaveJobDto.ApplicantId = base.CurrentUser.UserId;
                _responseDto.Result = await _jobService.SaveJobAsync(applicantSaveJobDto);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Job Saved Successfull";
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
                var activeJobs = await _jobService.GetActiveJobsAsync(paginationRequestDto);
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
