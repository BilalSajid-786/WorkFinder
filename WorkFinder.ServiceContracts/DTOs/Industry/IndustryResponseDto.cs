using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Industry
{
    public class IndustryResponseDto
    {
        public int IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
    }
}
