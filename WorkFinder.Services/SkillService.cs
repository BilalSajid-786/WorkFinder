using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Entities.Entities.SystemSeeding;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for skills
    /// </summary>
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        public SkillService(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SkillResponseDto>> GetSkills()
        {
           var skills = await _skillRepository.GetSkills();
           return _mapper.Map<IEnumerable<SkillResponseDto>>(skills);
        }

        public async Task InsertSkill(SkillRequestDto skill)
        {
            await _skillRepository.InsertSkill(_mapper.Map<Skill>(skill));
        }

        public async Task SeedSkillsAsync()
        {
            var skills = await _skillRepository.GetSkills();
            

            if(skills.Count() == 0)
            {
                var predefinedSkills = SystemSkills.Skills.OrderBy(s => s);
                foreach (var skill in predefinedSkills)
                {

                    await _skillRepository.InsertSkill(_mapper.Map<Skill>(skill));
                }
            }
        }
    }
}
