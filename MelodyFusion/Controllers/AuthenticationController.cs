using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelodyFusion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILoginService _loginService;

        public AuthenticationController(IRegistrationService registrationService, ILoginService loginService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var result = await _registrationService.Registration(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _loginService.Login(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

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
