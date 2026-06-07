using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.DTOs.User;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;
        public EmployersController(IEmployerService employerService, IAuthService authService)
        {
            _employerService = employerService;
            _authService = authService;
            _responseDto = new();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerResponseDto>>> GetAllEmployers()
        {
            var employers = await _employerService.GetAllEmployers();
            return Ok(employers);
        }

        [HttpPost("{employerId:Guid}")]
        public async Task<ActionResult<ResponseDto>> EditEmployer(Guid employerId,[FromBody] UpdateEmployerRequestDto employerRequest)
        {
            try
            {
                var employer = await _employerService.EditEmployerAsync(employerId, employerRequest,_authService);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                _responseDto.Result = employer;
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("{employerId:Guid}")]
        public async Task<ActionResult<EmployerResponseDto>> GetEmployerById(Guid employerId)
        {
            var employer = await _employerService.GetEmployerByIdAsync(employerId);
            return Ok(employer);
        }
    }
}
