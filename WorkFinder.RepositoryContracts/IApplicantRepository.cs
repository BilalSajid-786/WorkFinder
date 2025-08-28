using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Repository Contract for Applicant
    /// </summary>
    public interface IApplicantRepository
    {
        /// <summary>
        /// Insert the applicant into the database
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns>Applicant Id</returns>
        Task<Guid> InsertApplicantAsync(Applicant applicant);
    }
}
