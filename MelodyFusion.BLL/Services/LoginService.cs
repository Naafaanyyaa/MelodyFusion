using System.IdentityModel.Tokens.Jwt;
using MelodyFusion.BLL.Infrastructure;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MelodyFusion.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<UserDto> _userManager;
        private readonly IAuthenticationStatisticRepository _authenticationStatisticRepository;

        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<UserDto> userManager, JwtHandler jwtHandler, IAuthenticationStatisticRepository authenticationStatisticRepository)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _authenticationStatisticRepository = authenticationStatisticRepository;
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

            await _authenticationStatisticRepository.AddAsync(new AuthenticationStatisticDto(true, user.Id));

            return new LoginResponse() { IsAuthSuccessful = true, Token = token };
        }
    }
}
