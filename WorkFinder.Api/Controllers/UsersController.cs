using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.Entities.Entities;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.User;
using WorkFinder.Services;

namespace WorkFinder.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IModuleService _moduleService;
        public UsersController(IUserService userService, IModuleService moduleService)
        {
            _userService = userService;
            _moduleService = moduleService;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>Users</returns>
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

        /// <summary>
        /// Get SideBar Items for a logged in User
        /// </summary>
        /// <returns>SideBar Items</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentModuleResponseDto>>> GetSideBarItems()
        {
            return Ok(await _moduleService.GetSideBarItemsAsync(CurrentUser.RoleId));
        }
    }
}

