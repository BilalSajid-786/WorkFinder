using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.Common.Dtos.Applicants;
using WorkFinder.Common.Dtos.Jobs;
using WorkFinder.Common.Dtos.Pagination;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : BaseApiController
    {
        private readonly IApplicantService _applicantService;
        private readonly ResponseDto _responseDto;
        public ApplicantsController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
            _responseDto = new();
        }

        /// <summary>
        /// Get Applicants
        /// </summary>
        /// <param name="applicantsRequestDto"></param>
        /// <returns></returns

        [Authorize(Policy = "Applicant.Get")]
        [HttpPost("GetApplicants")]

        public async Task<ActionResult<ResponseDto>> GetApplicantsAsync(PaginationParameters<ApplicantsFilter> applicantsRequestDto)
        {
            try
            {
                var applicants = await _applicantService.GetApplicantsAsync(applicantsRequestDto);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = applicants;
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
