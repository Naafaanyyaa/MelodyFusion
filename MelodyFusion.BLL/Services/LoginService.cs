using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MelodyFusion.BLL.Infrastructure;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Identity;
using PetHospital.Data.Entities.Identity;

namespace MelodyFusion.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<UserDto> _userManager;

        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<UserDto> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new LoginResponse()
                {
                    ErrorMessage = "Invalid Authentication"
                };
            }

            if (user.IsBanned == true)
            {
                return new LoginResponse()
                {
                    ErrorMessage = "User is banned"
                };
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaimsAsync(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new LoginResponse() { IsAuthSuccessful = true, Token = token };
        }
    }
}
