using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponse> Registration(RegistrationRequest request);
    }
}