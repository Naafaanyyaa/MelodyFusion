using AutoMapper;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.Enums;
using MelodyFusion.DLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Policy;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Web;
using static System.Net.WebRequestMethods;

namespace MelodyFusion.BLL.Services
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserDto> _userManager;
        private readonly ILogger<RegistrationService> _logger;
        private readonly IAuthenticationStatisticRepository _authenticationStatisticRepository;
        private readonly IEmailSender _emailSender;

        public RegistrationService(IMapper mapper, 
            UserManager<UserDto> userManager, 
            IAuthenticationStatisticRepository authenticationStatisticRepository,
            ILogger<RegistrationService> logger, 
            IEmailSender emailSender)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _authenticationStatisticRepository = authenticationStatisticRepository;
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

            await GenerateEmailConfirmationTokenAsync(user);

            await _authenticationStatisticRepository.AddAsync(new AuthenticationStatisticDto(false, user.Id));

            var result = _mapper.Map<UserDto, RegistrationResponse>(user);

            return result;
        }

        private async Task GenerateEmailConfirmationTokenAsync(UserDto user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var tokenHtmlVersion = HttpUtility.UrlEncode(token);

            var confirmationLink = $"https://localhost:7293/api/Authentication/mail-confirmation?token={tokenHtmlVersion}&email={user.Email}";

            await _emailSender.SendEmailAsync(user.Email!, "Confirmation email link", $"Please follow by this link to confirm your mail: \n{confirmationLink}");
        }
    }
}
