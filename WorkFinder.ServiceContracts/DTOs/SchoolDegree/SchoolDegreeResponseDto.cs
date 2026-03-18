using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.SchoolDegree
{
    public class SchoolDegreeResponseDto
    {
        public int SchoolDegreeId { get; set; }
        public string SchoolDegreeName { get; set; } = string.Empty;
    }
}
