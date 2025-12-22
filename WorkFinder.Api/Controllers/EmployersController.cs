using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Employer;
using WorkFinder.ServiceContracts.DTOs.User;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IAuthService _authService;
        public EmployersController(IEmployerService employerService, IAuthService authService)
        {
            _employerService = employerService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerResponseDto>>> GetAllEmployers()
        {
            var employers = await _employerService.GetAllEmployers();
            return Ok(employers);
        }

        [HttpPost("{employerId:Guid}")]
        public async Task<ActionResult<string>> EditEmployer(Guid employerId,[FromBody] UpdateEmployerRequestDto employerRequest)
        {
            var result = await _employerService.EditEmployerAsync(employerId, employerRequest,_authService);
            return Ok(new {result = result});
        }

        [HttpGet("{employerId:Guid}")]
        public async Task<ActionResult<EmployerResponseDto>> GetEmployerById(Guid employerId)
        {
            var employer = await _employerService.GetEmployerByIdAsync(employerId);
            return Ok(employer);
        }
    }
}
