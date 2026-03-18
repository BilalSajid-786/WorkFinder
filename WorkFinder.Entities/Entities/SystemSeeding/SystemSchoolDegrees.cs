using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public class SystemSchoolDegrees
    {
        public static Dictionary<int, string> SchoolDegrees { get; set; }
        static SystemSchoolDegrees()
        {
            SchoolDegrees = new Dictionary<int, string>()
            {
                {1,"No School-Leaving Certificate" },
                {2, "Lower Secondary School Certificate" },
                {3, "Secondary School Certificate" },
                {4, "High School Diploma" }
            };
        }
    }
}
