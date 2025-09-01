using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Industry;

namespace WorkFinder.ServiceContracts
{
    public interface IIndustryService
    {
        /// <summary>
        /// Inserts a new industry in the system
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        Task InsertIndustry(IndustryRequestDto industryRequest);

        /// <summary>
        /// Gets all industries from the system
        /// </summary>
        /// <returns>Industries</returns>
        Task<IEnumerable<IndustryResponseDto>> GetIndustries();

        /// <summary>
        /// Seed industries if there are no indutries available.
        /// </summary>
        /// <returns></returns>
        Task SeedIndustriesAsync();
    }
}
