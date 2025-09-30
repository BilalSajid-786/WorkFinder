using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public class SystemQualifications
    {
        public static Dictionary<int, string> Qualifications { get; set; }
        static SystemQualifications()
        {
            Qualifications = new Dictionary<int, string>()
            {
                {1,"Matric" },
                {2, "Inter" },
                {3, "Fsc" }
            };
        }
    }
}
