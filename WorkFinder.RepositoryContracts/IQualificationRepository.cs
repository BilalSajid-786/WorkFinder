using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Qualification
    /// </summary>
    public interface IQualificationRepository
    {
        /// <summary>
        /// Insert qualification into db
        /// </summary>
        /// <param name="qualification"></param>
        /// <returns></returns>
        Task<int> InsertQualification(Qualification qualification);

        /// <summary>
        /// Get all qualifications from db
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Qualification>> GetAllQualifications();
    }
}
