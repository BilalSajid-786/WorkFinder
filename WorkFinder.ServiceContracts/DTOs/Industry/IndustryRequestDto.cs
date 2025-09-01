using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Industry
{
    public class IndustryRequestDto
    {
        [Required]
        public string IndustryName { get; set; } = string.Empty;
    }
}
