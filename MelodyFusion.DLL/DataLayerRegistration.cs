using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHospital.Data.Entities.Identity;

namespace MelodyFusion.DLL
{
    public static class DataLayerRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserDto, RoleDto>()
                .AddUserStore<UserStore<UserDto, RoleDto, ApplicationDbContext, string, IdentityUserClaim<string>, UserRole,
                    IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
                .AddRoleStore<RoleStore<RoleDto, ApplicationDbContext, string, UserRole, IdentityRoleClaim<string>>>()
                .AddSignInManager<SignInManager<UserDto>>()
                .AddRoleManager<RoleManager<RoleDto>>()
                .AddUserManager<UserManager<UserDto>>();

            return services;
        }
    }
}
