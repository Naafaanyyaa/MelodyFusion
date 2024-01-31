using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Enums;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<UserResponse> ChangeUserAccess(string userId);
        Task<UserResponse> AddRole(string userId, RoleEnum role);
        Task<UserResponse> DeleteRole(string userId, RoleEnum role);
        Task<List<UserResponse>> GetUserListByRequest(UserAllRequest request);
    }
}
