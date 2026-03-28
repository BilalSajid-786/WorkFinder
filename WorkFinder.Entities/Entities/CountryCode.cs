using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class CountryCode
    {
        public string CountryCodeId { get; set; }
        public string CallingCode { get; set; } = string.Empty;
    }
}
