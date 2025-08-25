using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public static class SystemCountries
    {
        public static Dictionary<int,string> Countries { get; set; }

        static SystemCountries()
        {
            Countries = new Dictionary<int, string>()
            {
                {1,"United States" },
                {2, "United Kingdom" },
                {3, "Canada" },
                {4, "Australia" },
                {5, "Germany" }
            };
        }
    }
}
