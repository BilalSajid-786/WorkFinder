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
using WorkFinder.ServiceContracts.DTOs.CountryCode;

namespace WorkFinder.Services
{
    public class CountryCodeService : ICountryCodeService
    {
        private readonly ICountryCodeRepository _countryCodeRepository;
        private readonly IMapper _mapper;

        public CountryCodeService(ICountryCodeRepository countryCodeRepository,IMapper mapper)
        {
            _countryCodeRepository = countryCodeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryCodeResponseDto>> GetAllCountryCode()
        {
            var countryCodes = await _countryCodeRepository.GetAllCountryCode();
            return _mapper.Map<IEnumerable<CountryCodeResponseDto>>(countryCodes);
        }

        public async Task SeedCountryCode()
        {
            var countryCodes = await _countryCodeRepository.GetAllCountryCode();

            if(countryCodes.Count() == 0)
            {
                var systemCountryCodes = SystemCountryCodes.CallingCodes;
                foreach (var countryCode in systemCountryCodes)
                {
                    await _countryCodeRepository.InsertCountryCode(new CountryCode()
                    {
                        CountryCodeId = countryCode.Key,
                        CallingCode = countryCode.Value
                    });
                }
            }
        }
    }
}
