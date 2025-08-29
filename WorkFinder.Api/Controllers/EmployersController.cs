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
        public EmployersController(IEmployerService employerService)
        {
                _employerService = employerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerResponseDto>>> GetAllEmployers()
        {
            var employers = await _employerService.GetAllEmployers();
            return Ok(employers);
        }

        [HttpPatch("{userId:Guid}")]
        public async Task<ActionResult<int>> EditEmployer(Guid userId, [FromBody] EmployerRequestDto employerRequest)
        {
            var result = await _employerService.EditEmployerAsync(userId, employerRequest);
            return Ok(result);
        }

        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<EmployerResponseDto>> GetEmployerById(Guid userId)
        {
            var employer = await _employerService.GetEmployerByIdAsync(userId);
            return Ok(employer);
        }

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> DeleteEmployer(Guid userId)
        {
            var isDeleted = await _employerService.DeleteEmployerAsync(userId);
            return Ok(isDeleted);
        }

        [HttpPatch("{userId}/status")]
        public async Task<ActionResult<bool?>> UpdateEmployerStatus(Guid userId, [FromBody] bool isActive)
        {
            var result = await _employerService.UpdateEmployerStatusAsync(userId, isActive);
            return Ok(result);
        }
    }
}
