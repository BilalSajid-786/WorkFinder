using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Entities.Entities;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates the user with the given email and password
        /// </summary>
        /// <param name="email">email of the user attempting to login</param>
        /// <returns>Jwt token, if valid</returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login(string email)
        {
            var token = await _authService.AuthenticateAsync(email,null);
            return Ok(new {token = token});
        }
    }
}
