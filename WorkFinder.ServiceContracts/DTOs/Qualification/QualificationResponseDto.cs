using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Qualification
{
    public class QualificationResponseDto
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; } = string.Empty;
    }
}
