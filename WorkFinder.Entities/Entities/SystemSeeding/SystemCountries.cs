using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public static class SystemCountries
    {
        public static Dictionary<int, string> Countries { get; set; }

        static SystemCountries()
        {
            Countries = new Dictionary<int, string>()
            {
                // existing
                { 1, "United States" },
                { 2, "United Kingdom" },
                { 3, "Canada" },
                { 4, "Australia" },
                { 5, "Germany" },

                // Europe (new) — IDs continue from 6
                { 6,  "Albania" },
                { 7,  "Andorra" },
                { 8,  "Austria" },
                { 9,  "Belarus" },
                { 10, "Belgium" },
                { 11, "Bosnia and Herzegovina" },
                { 12, "Bulgaria" },
                { 13, "Croatia" },
                { 14, "Cyprus" },
                { 15, "Czechia" },                 // (Czech Republic)
                { 16, "Denmark" },
                { 17, "Estonia" },
                { 18, "Finland" },
                { 19, "France" },
                { 20, "Greece" },
                { 21, "Hungary" },
                { 22, "Iceland" },
                { 23, "Ireland" },
                { 24, "Italy" },
                { 25, "Latvia" },
                { 26, "Liechtenstein" },
                { 27, "Lithuania" },
                { 28, "Luxembourg" },
                { 29, "Malta" },
                { 30, "Moldova" },
                { 31, "Monaco" },
                { 32, "Montenegro" },
                { 33, "Netherlands" },
                { 34, "North Macedonia" },
                { 35, "Norway" },
                { 36, "Poland" },
                { 37, "Portugal" },
                { 38, "Romania" },
                { 39, "Russia" },
                { 40, "San Marino" },
                { 41, "Serbia" },
                { 42, "Slovakia" },
                { 43, "Slovenia" },
                { 44, "Spain" },
                { 45, "Sweden" },
                { 46, "Switzerland" },
                { 47, "Ukraine" },
                { 48, "Vatican City" },

                // Transcontinental/Political Europe (commonly included)
                { 49, "Kosovo" },                  // limited recognition
                { 50, "Georgia" },
                { 51, "Armenia" },
                { 52, "Azerbaijan" },
                { 53, "Turkey" }
            };
        }
    }
}
