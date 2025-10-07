using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Country
{
    public class CountryResponseDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
}
