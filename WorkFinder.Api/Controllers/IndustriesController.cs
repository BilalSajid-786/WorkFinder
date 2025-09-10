using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Industry;
using WorkFinder.ServiceContracts.DTOs.Skill;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustriesController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpGet]
        public async Task<ActionResult<IndustryResponseDto>> GetIndustries()
        {
            return Ok(await _industryService.GetIndustries());
        }
    }
}
