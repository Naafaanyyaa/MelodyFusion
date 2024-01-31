using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetHospital.Data.Entities.Identity;

namespace MelodyFusion.BLL.Infrastructure
{
    public class JwtHandler 
    {
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<UserDto> _userManager;

        public JwtHandler(IConfiguration configuration, UserManager<UserDto> userManager)
        {
            _jwtSettings = configuration.GetSection("Jwt");
            _userManager = userManager;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["Key"]);
            if (key.Length < 32)
            {
                throw new ArgumentException("JWT key must be at least 256 bits (32 bytes).");
            }
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<List<Claim>> GetClaimsAsync(UserDto user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}