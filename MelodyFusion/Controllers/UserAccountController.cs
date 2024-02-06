using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelodyFusion.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.GetUserInfoAsync(userId);
            return Ok(result);
        }

        [HttpPatch("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.ChangePasswordAsync(userId, newPassword);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _userService.DeleteAsync(userId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfo(UserRequest userRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.UpdateAsync(userId, userRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PhotoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeAvatar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.ChangeAvatar(Request.Form.Files, userId);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
