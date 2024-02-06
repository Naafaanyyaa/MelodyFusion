using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using Microsoft.AspNetCore.Http;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserInfoAsync(string userId);
        Task<UserResponse> UpdateAsync(string userId, UserRequest userRequest);
        Task DeleteAsync(string userId);
        Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordRequest passwordRequest);
        Task<PhotoResponse> ChangeAvatar(IFormFileCollection files, string userId);
    }
}