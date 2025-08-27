using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Employer
{
    public class EmployerResponseDto
    {
        public Guid UserId { get; set; }
        public Guid EmployerId { get; set; }
    }
}
