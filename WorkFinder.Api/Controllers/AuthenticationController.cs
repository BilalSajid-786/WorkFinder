using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = await _userRepository.RegisterUserAsync(new User());
            return user;
        }
    }
}
