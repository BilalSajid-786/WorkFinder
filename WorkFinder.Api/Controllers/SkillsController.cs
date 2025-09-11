using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<SkillResponseDto>> GetSkills()
        {
            return Ok(await _skillService.GetSkills());
        }

        /// <summary>
        /// Search skills by given name
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HttpGet("{searchName}")]
        public async Task<ActionResult<ResponseDto>> GetSkillByName(string searchName)
        {
            var skills = await _skillService.GetSkillByName(searchName);
            return new ResponseDto()
            {
                Result = skills,
                IsSuccess = true,
                Message = "Success"
            };
        }

        [HttpPost]
        public async Task<ActionResult<string>> InsertSkill(SkillRequestDto skillRequestDto)
        {
            await _skillService.InsertSkill(skillRequestDto);
            return Ok("Skill created Successfully");
        }
    }
}
