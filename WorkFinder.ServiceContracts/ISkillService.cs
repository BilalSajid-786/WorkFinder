using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Skill;

namespace WorkFinder.ServiceContracts
{
    public interface ISkillService
    {
        /// <summary>
        /// Inserts a new skill in the system
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        Task InsertSkill(SkillRequestDto skill);

        /// <summary>
        /// Gets all skills from the system
        /// </summary>
        /// <returns>Skills</returns>
        Task<IEnumerable<SkillResponseDto>> GetSkills();

        /// <summary>
        /// Seed skills if there are no skills available.
        /// </summary>
        /// <returns></returns>
        Task SeedSkillsAsync();
    }
}
