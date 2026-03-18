using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Contract for School Degree Repository
    /// </summary>
    public interface ISchoolDegreeRepository
    {
        /// <summary>
        /// Insert school degree into the system
        /// </summary>
        /// <param name="schoolDegree"></param>
        /// <returns></returns>
        Task<int> InsertSchoolDegree(SchoolDegree schoolDegree);

        /// <summary>
        /// Get all school degrees from the system
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SchoolDegree>> GetAllSchoolDegrees();
    }
}
