using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;
        private readonly ResponseDto _responseDto;
        public QualificationsController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
            _responseDto = new();
        }

        /// <summary>
        /// Get available qualifications.
        /// </summary>
        /// <returns>Qualifications</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetQualificationsAsync()
        {
            try
            {
                var qualifications = await _qualificationService.GetQualificationsAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = qualifications;
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
