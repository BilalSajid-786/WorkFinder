using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class ApplicantSkill
    {
        public Guid ApplicantId { get; set; }
        public int SkillId { get; set; }
        public Applicant? Applicant { get; set; }
        public Skill? Skill { get; set; }
    }
}
