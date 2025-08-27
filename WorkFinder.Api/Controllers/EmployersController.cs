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
    }
}
