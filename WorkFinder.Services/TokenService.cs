using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public TokenService(IConfiguration configuration, IRoleService roleService)
        {
            _configuration = configuration;
            _roleService = roleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateToken(UserResponseDto user)
        {
            //security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var permissions = await _roleService.GetRolePermissionsByRoleIdAsync(user.RoleId);

            //claims
            var claims = new List<Claim>()
            {
                new Claim("UserId",user.UserId.ToString()),
                new Claim("RoleId",user.RoleId.ToString()),
                new Claim("UserRole",user.RoleName),
                new Claim("BaseUserId",user.BaseUserId.ToString())
            };

            //permissions
            foreach (var permission in permissions) 
            {
                claims.Add(new Claim("Permissions", permission.Action));
            }

            //token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Double.Parse(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
