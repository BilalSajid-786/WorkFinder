using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository contract for Skill Entity database operations
    /// </summary>
    public interface ISkillRepository
    {
        /// <summary>
        /// Inserts a new skill in the database
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        Task<int> InsertSkill(Skill skill);

        /// <summary>
        /// Gets all skills from the database
        /// </summary>
        /// <returns>Skills</returns>
        Task<IEnumerable<Skill>> GetSkills();

        /// <summary>
        /// Get skill by search name
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        Task<IEnumerable<Skill>> GetSkillByName(string searchName);
    }
}
