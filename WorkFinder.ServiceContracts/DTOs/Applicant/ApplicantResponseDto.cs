using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.DTOs.Authentication;

namespace WorkFinder.ServiceContracts.DTOs.Applicant
{
    public class ApplicantResponseDto
    {
        public Guid ApplicantId { get; set; }
        public Guid UserId { get; set; }
    }
}
