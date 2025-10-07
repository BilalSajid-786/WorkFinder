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

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for Qualification
    /// </summary>
    public class QualificationService : IQualificationService
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IMapper _mapper;
        public QualificationService(IQualificationRepository qualificationRepository, IMapper mapper)
        {
            _qualificationRepository = qualificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get qualifications from the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<QualificationResponseDto>> GetQualificationsAsync()
        {
            var qualifications = await _qualificationRepository.GetAllQualifications();
            return _mapper.Map<IEnumerable<QualificationResponseDto>>(qualifications);

        }

        /// <summary>
        /// Seeds qualification into the system
        /// </summary>
        /// <returns></returns>
        public async Task SeedQualficationAsync()
        {
            var qualifications = await _qualificationRepository.GetAllQualifications();
            if(qualifications.Count() == 0)
            {
                foreach (var qualification in SystemQualifications.Qualifications)
                {

                    await _qualificationRepository.InsertQualification(new Qualification()
                    {
                        QualificationId = qualification.Key,
                        QualificationName  = qualification.Value
                    });
                }
            }
        }
    }
}
