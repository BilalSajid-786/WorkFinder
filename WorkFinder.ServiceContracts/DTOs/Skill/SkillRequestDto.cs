using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Skill
{
    public class SkillRequestDto
    {
        [Required]
        public string SkillName { get; set; } = string.Empty;
    }
}
