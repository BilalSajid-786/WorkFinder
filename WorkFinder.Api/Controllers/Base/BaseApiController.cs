using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.Api.Controllers.Base
{

    [ApiController]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        protected CurrentUser CurrentUser
        {
            get
            {
                return new()
                {
                    UserId = Guid.TryParse(HttpContext.User.FindFirstValue("UserId"),out var userId)? userId : Guid.Empty,
                    UserEmail = HttpContext.User.FindFirstValue("UserEmail") ?? string.Empty,
                    BaseUserId = Guid.TryParse(HttpContext.User.FindFirstValue("BaseUserId"), out var baseUserId) ? baseUserId : Guid.Empty,
                    RoleId = Guid.TryParse(HttpContext.User.FindFirstValue("RoleId"),out var roleId) ? roleId : Guid.Empty,
                    UserRole = HttpContext.User.FindFirstValue("UserRole") ?? string.Empty
                };
            }
        }
    }
}
