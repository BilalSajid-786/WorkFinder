using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Industry;

namespace WorkFinder.Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepository _industryRepository;
        private readonly IMapper _mapper;
        public IndustryService(IIndustryRepository industryRepository, IMapper mapper)
        {
            _industryRepository = industryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<IndustryResponseDto>> GetIndustries()
        {
            var industries = await _industryRepository.GetIndustries();
            return _mapper.Map<IEnumerable<IndustryResponseDto>>(industries);
        }

        public async Task InsertIndustry(IndustryRequestDto industryRequest)
        {
            await _industryRepository.InsertIndustry(_mapper.Map<Industry>(industryRequest));
        }

        public async Task SeedIndustriesAsync()
        {
            var industries = await _industryRepository.GetIndustries();


            if (industries.Count() == 0)
            {
                var predefinedIndustries = SystemIndustries.Industries.OrderBy(i => i);
                foreach (var industry in predefinedIndustries)
                {

                    await _industryRepository.InsertIndustry(_mapper.Map<Industry>(industry));
                }
            }
        }
    }
}
