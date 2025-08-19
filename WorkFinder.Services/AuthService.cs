using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;

namespace WorkFinder.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public AuthService(ITokenService tokenService, IUserRepository userRepository)
        {

            _tokenService = tokenService;
            _userRepository = userRepository;

        }
        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null)
                return null;
            return _tokenService.GenerateToken(user);
        }
    }
}
