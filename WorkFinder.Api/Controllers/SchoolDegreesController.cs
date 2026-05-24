using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.DTOs.SchoolDegree;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchoolDegreesController : ControllerBase
    {
        private readonly ISchoolDegreeService _schoolDegreeService;
        private readonly ResponseDto _responseDto;
        public SchoolDegreesController(ISchoolDegreeService schoolDegreeService)
        {
            _schoolDegreeService = schoolDegreeService;
            _responseDto = new ResponseDto();
        }

        /// <summary>
        /// Get School Degrees from the System
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllSchoolDegrees()
        {
            try
            {
                var schoolDegrees = await _schoolDegreeService.GetSchoolDegreesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = schoolDegrees;
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
