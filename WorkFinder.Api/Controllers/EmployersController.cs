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

        [HttpPatch("{employerId:Guid}")]
        public async Task<ActionResult<string>> EditEmployer(Guid employerId, [FromBody] EmployerRequestDto employerRequest)
        {
            var result = await _employerService.EditEmployerAsync(employerId, employerRequest);
            return Ok(result);
        }

        [HttpGet("{employerId:Guid}")]
        public async Task<ActionResult<EmployerResponseDto>> GetEmployerById(Guid employerId)
        {
            var employer = await _employerService.GetEmployerByIdAsync(employerId);
            return Ok(employer);
        }

        //[HttpDelete("{employerId:Guid}")]
        //public async Task<IActionResult> DeleteEmployer(Guid employerId)
        //{
        //    var isDeleted = await _employerService.DeleteEmployerAsync(employerId);
        //    return Ok(isDeleted);
        //}

        //[HttpPatch("{userId}/status")]
        //public async Task<ActionResult<bool?>> UpdateEmployerStatus(Guid userId, [FromBody] bool isActive)
        //{
        //    var result = await _employerService.UpdateEmployerStatusAsync(userId, isActive);
        //    return Ok(result);
        //}
    }
}
