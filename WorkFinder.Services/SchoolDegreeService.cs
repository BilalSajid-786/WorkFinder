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
using WorkFinder.ServiceContracts.DTOs.Qualification;
using WorkFinder.ServiceContracts.DTOs.SchoolDegree;

namespace WorkFinder.Services
{
    /// <summary>
    /// Implementation of School Degree Service
    /// </summary>
    public class SchoolDegreeService : ISchoolDegreeService
    {
        private readonly ISchoolDegreeRepository _schoolDegreeRepository;
        private readonly IMapper _mapper;
        public SchoolDegreeService(ISchoolDegreeRepository schoolDegreeRepository, IMapper mapper)
        {
            _schoolDegreeRepository = schoolDegreeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SchoolDegreeResponseDto>> GetSchoolDegreesAsync()
        {
            var schoolDegrees = await _schoolDegreeRepository.GetAllSchoolDegrees();
            return _mapper.Map<IEnumerable<SchoolDegreeResponseDto>>(schoolDegrees);
        }

        public async Task SeedSchoolDegreesAsync()
        {
            var schoolDegrees = await _schoolDegreeRepository.GetAllSchoolDegrees();
            if (schoolDegrees.Count() == 0)
            {
                foreach (var schoolDegree in SystemSchoolDegrees.SchoolDegrees)
                {

                    await _schoolDegreeRepository.InsertSchoolDegree(new SchoolDegree()
                    {
                        SchoolDegreeId = schoolDegree.Key,
                        SchoolDegreeName = schoolDegree.Value
                    });
                }
            }
        }
    }
}
