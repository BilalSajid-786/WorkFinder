using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.CountryCode
{
    public class CountryCodeResponseDto
    {
        public string CountryCodeId { get; set; } = string.Empty;
        public string CallingCode { get; set; } = string.Empty;
    }
}
