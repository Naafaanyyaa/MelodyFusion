using System.Security.Claims;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;

namespace MelodyFusion.BLL.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
