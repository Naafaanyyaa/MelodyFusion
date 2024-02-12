using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelodyFusion.Controllers
{
    /// <summary>
    /// Controller for handling user account operations.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountController"/> class.
        /// </summary>
        /// <param name="userService">The service for user account operations.</param>
        public UserAccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves user information.
        /// </summary>
        /// <returns>An IActionResult representing the user information.</returns>
        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.GetUserInfoAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Updates user password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <returns>An IActionResult representing the result of password update.</returns>
        [HttpPatch("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.ChangePasswordAsync(userId, newPassword);
            return Ok(result);
        }

        /// <summary>
        /// Deletes user account.
        /// </summary>
        /// <returns>An IActionResult representing the result of account deletion.</returns>
        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _userService.DeleteAsync(userId);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="userRequest">The request containing updated user information.</param>
        /// <returns>An IActionResult representing the result of user information update.</returns>
        [HttpPut("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfo(UserRequest userRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.UpdateAsync(userId, userRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Changes user avatar.
        /// </summary>
        /// <returns>An IActionResult representing the result of avatar change.</returns>
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
