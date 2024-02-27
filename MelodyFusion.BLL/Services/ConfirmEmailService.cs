using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.DLL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace MelodyFusion.BLL.Services
{
    public class ConfirmEmailService : IConfirmationEmailService
    {
        private readonly UserManager<UserDto> _userManager;

        public ConfirmEmailService(UserManager<UserDto> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }
    }
}
