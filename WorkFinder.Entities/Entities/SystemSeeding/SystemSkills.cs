using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public static class SystemSkills
    {
        public static List<string> Skills { get; set; }
        static SystemSkills()
        {
            Skills = new List<string>()
             {
               // Tech & IT Skills
               "Web Development",
               "Mobile App Development",
               "Backend Development",
               "Database Management",
               "Cloud Computing",
               "Cybersecurity",
               "DevOps",

               // Design & Creative Skills
               "Graphic Design",
               "UI/UX Design",
               "Video Editing",
               "Animation / Motion Graphics",
               "Photography",
               "3D Modeling",

               // Business & Marketing Skills
               "Digital Marketing",
               "SEO",
               "Social Media Marketing",
               "Content Writing",
               "Copywriting",
               "Email Marketing",
               "Project Management",
               "Business Analysis",

               // Data & Analytics Skills
               "Data Analysis",
               "Data Science",
               "Machine Learning",
               "Artificial Intelligence",
               "Power BI",
               "Tableau",
               "Excel",

               // General Professional Skills
               "Communication Skills",
               "Customer Service",
               "Sales",
               "Teaching",
               "Training",
               "Translation",
               "Virtual Assistance"
             };
        }
    }
}
