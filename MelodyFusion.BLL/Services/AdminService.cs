using AutoMapper;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Enums;
using Microsoft.AspNetCore.Identity;
using PetHospital.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace MelodyFusion.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<UserDto> _userManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<UserDto> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserResponse> ChangeUserAccess(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.IsBanned = !user.IsBanned;

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> AddRole(string userId, RoleEnum role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var rolesList = await _userManager.GetRolesAsync(user);

            if (rolesList.Contains(role.ToString()))
            {
                throw new ValidationException($"User already has role {role.ToString()}");
            }

            await _userManager.AddToRolesAsync(user, new List<string>
            {
                role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                }
            });

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> DeleteRole(string userId, RoleEnum role)
        {
            if (role == RoleEnum.User)
            {
                throw new ValidationException("It is not allowed to delete role \"User\"");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var rolesList = await _userManager.GetRolesAsync(user);

            if (!rolesList.Contains(role.ToString()))
            {
                throw new ValidationException($"User does not have role \"{role.ToString()}\"");
            }

            await _userManager.RemoveFromRolesAsync(user, new List<string>
            {
                role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                }
            });

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task<List<UserResponse>> GetUserListByRequest(UserAllRequest request)
        {
            var userList = await _userManager.Users
                .Where(x => string.IsNullOrEmpty(request.SearchRequest) || x.UserName.Contains(request.SearchRequest) || x.Email.Contains(request.SearchRequest))
                .Skip(request.PageFrom * request.PageSize)
                .Take(request.PageSize) 
                .ToListAsync();

            var result = _mapper.Map<List<UserDto>, List<UserResponse>>(userList);

            return result;
        }
    }
}
