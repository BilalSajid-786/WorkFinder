using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Skill
{
    public class SkillResponseDto
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
    }
}
