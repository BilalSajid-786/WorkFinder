using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public class SystemIndustries
    {
        public static List<string> Industries { get; set; }
        static SystemIndustries()
        {
            Industries = new List<string>()
            {
               "Renewable Energy/Environmental Technology",
               "Aerospace",
               "Finance/Insurance/Real Estate",
               "Manufacturing",
               "Healthcare/Medical Technology",
               "Chemical & Pharmaceutical",
               "ICT/Software & Telecommunications",
               "Retail & Wholesale",
               "Services",
               "Metals/Mining",
               "Construction",
               "Food & Beverage/Agriculture",
               "Electronics & Electrical Engineering",
               "Logistics & Transportation",
               "Mechanical Engineering",
               "Automotive"
            };
        }
    }
}
