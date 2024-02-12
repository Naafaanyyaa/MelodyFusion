using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelodyFusion.Controllers
{
    /// <summary>
    /// Controller for handling administrative operations.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AdminController : ControllerBase 
    {
        private readonly IAdminService _adminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="adminService">The service for administrative operations.</param>
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Bans a user.
        /// </summary>
        /// <param name="userId">The ID of the user to ban.</param>
        /// <returns>A response containing the updated user details.</returns>
        [HttpPatch("[action]/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> BanUser(string userId)
        {
            var result = await _adminService.ChangeUserAccess(userId);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        /// <param name="request">The request containing user ID and role to add.</param>
        /// <returns>A response containing the updated user details.</returns>
        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddRole([FromBody] ChangeRoleRequest request)
        {
            var result = await _adminService.AddRole(request.UserId, request.Role);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Deletes a role from a user.
        /// </summary>
        /// <param name="request">The request containing user ID and role to delete.</param>
        /// <returns>A response containing the updated user details.</returns>
        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteRole([FromBody] ChangeRoleRequest request)
        {
            var result = await _adminService.DeleteRole(request.UserId, request.Role);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Retrieves a list of users based on the specified criteria.
        /// </summary>
        /// <param name="request">The request containing criteria for user retrieval.</param>
        /// <returns>A list of users matching the specified criteria.</returns>
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
