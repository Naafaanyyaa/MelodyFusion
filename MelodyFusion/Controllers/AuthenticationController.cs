using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelodyFusion.Controllers
{
    /// <summary>
    /// Controller for handling user authentication operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILoginService _loginService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="registrationService">The service for user registration.</param>
        /// <param name="loginService">The service for user login.</param>
        public AuthenticationController(IRegistrationService registrationService, ILoginService loginService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing user information.</param>
        /// <returns>A response containing the newly registered user details.</returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var result = await _registrationService.Registration(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="request">The login request containing user credentials.</param>
        /// <returns>A response containing authentication token for the logged-in user.</returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _loginService.Login(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Handles Google login callback.
        /// </summary>
        /// <returns>An IActionResult representing the result of Google login callback.</returns>
        [HttpGet("signin-google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
            //Do something with the claims
            // var user = await UserService.FindOrCreate(new { name, givenName, email});

            return Ok();
        }
    }
}
