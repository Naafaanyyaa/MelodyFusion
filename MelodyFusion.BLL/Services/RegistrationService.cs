using AutoMapper;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHospital.Data.Entities.Identity;

namespace MelodyFusion.BLL.Services
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserDto> _userManager;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(IMapper mapper, UserManager<UserDto> userManager, ILogger<RegistrationService> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email || x.UserName == request.UserName);

            if (isUserExists)
            {
                throw new ValidationException("User with such username or email already exists");
            }

            UserDto user = new UserDto();

            _mapper.Map(request, user);

            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            identityResult = await _userManager.AddToRolesAsync(user, new List<string>
            {
                CustomRoles.UserRole,
                request.Role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                    _ => CustomRoles.UserRole
                }
            });

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            _logger.LogInformation("User {UserId} has been successfully registered", user.Id);

            var result = _mapper.Map<UserDto, RegistrationResponse>(user);

            return result;
        }
    }
}
