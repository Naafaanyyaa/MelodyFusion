using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelodyFusion.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AdminController : ControllerBase 
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPatch("[action]/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> BanUser(string userId)
        {
            var result = await _adminService.ChangeUserAccess(userId);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddRole([FromBody] ChangeRoleRequest request)
        {
            var result = await _adminService.AddRole(request.UserId, request.Role);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteRole([FromBody] ChangeRoleRequest request)
        {
            var result = await _adminService.DeleteRole(request.UserId, request.Role);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserList([FromQuery] UserAllRequest request)
        {
            var result = await _adminService.GetUserListByRequest(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
