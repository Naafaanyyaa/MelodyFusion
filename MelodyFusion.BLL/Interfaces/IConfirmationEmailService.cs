using Microsoft.AspNetCore.Identity;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IConfirmationEmailService
    {
        Task<IdentityResult> ConfirmEmail(string token, string email);
    }
}