using AutoMapper;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Infrastructure.StaticClasses;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MelodyFusion.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserDto> _userManager;
        private readonly IAzureBlobService _azureBlobService;
        private readonly IPhotoRepository _photoRepository;

        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<UserDto> userManager, IAzureBlobService azureBlobService, IPhotoRepository photoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _azureBlobService = azureBlobService;
            _photoRepository = photoRepository;
        }

        public async Task<UserResponse> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> UpdateAsync(string userId, UserRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }
            
            _mapper.Map<UserRequest, UserDto>(userRequest, user);

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var identityResult = await _userManager.DeleteAsync(user);

            if (identityResult.Errors.Any())
            {
                throw new Exception(identityResult.Errors.ToArray().ToString());
            }
        }

        public async Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordRequest passwordRequest)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, passwordRequest.OldPassword))
            {
                throw new ValidationException("Password doesn't match");
            }

            await _userManager.ChangePasswordAsync(user, passwordRequest.OldPassword, passwordRequest.NewPassword);

            var result = _mapper.Map<UserDto, UserResponse>(user);

            return result;
        }

        public async Task<PhotoResponse> ChangeAvatar(IFormFileCollection files, string userId)
        {
            if (!files.Any())
            {
                throw new ValidationException("There is no files");
            }

            var existingPhoto = await _photoRepository.GetAsync(x => x.UserId == userId);

            if (!existingPhoto.IsNullOrEmpty())
            {
                await _photoRepository.DeleteAsync(existingPhoto.First());
            }

            var uriList = await _azureBlobService.SavePhotoAsync(files, ContainerNames.UserContainer, userId);

            var entity = await _photoRepository.AddAsync(new PhotoDto
            {
                Uri = uriList.First().ToString(),
                UserId = userId
            });

            var result = _mapper.Map<PhotoDto, PhotoResponse>(entity);

            return result;
        }
    }
}
