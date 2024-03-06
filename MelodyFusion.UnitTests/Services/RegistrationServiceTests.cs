using AutoMapper;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.Enums;
using MelodyFusion.DLL.Interfaces;
using MelodyFusion.UnitTests.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MelodyFusion.UnitTests.Services
{
    public class RegistrationServiceTests : BaseUnitTest
    {
        private readonly Mock<UserManager<UserDto>> _userManagerMock = new();
        private readonly Mock<IAuthenticationStatisticRepository> _authenticationStatisticRepositoryMock = new();
        private readonly Mock<IRegistrationService> _registrationService = new();
        private readonly Mock<IRegistrationService> _registrationServiceMock = new Mock<IRegistrationService>();

        public RegistrationServiceTests()
        {
        
        }
        [Fact]
        public async Task Should_Throw_ValidationException_When_Register()
        {
            var request = new RegistrationRequest
            {
                UserName = "existingUser",
                Email = "existing@example.com",
                Password = "password",
                Role = RoleEnum.User
            };

            _registrationServiceMock.Setup(x => x.Registration(request))
                .ThrowsAsync(new ValidationException("User with such username or email already exists"));
            ;

        }
    }
}
