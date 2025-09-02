using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.User;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var isDeleted = await _userService.DeleteUserAsync(userId);
            return Ok(isDeleted);
        }

        [HttpPatch("{userId}/status")]
        public async Task<ActionResult<bool?>> UpdateUserStatus(Guid userId, [FromBody] bool isActive)
        {
            var result = await _userService.UpdateUserStatusAsync(userId, isActive);
            return Ok(result);
        }
    }
}

