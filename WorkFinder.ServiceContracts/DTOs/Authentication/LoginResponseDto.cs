using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Authentication
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
