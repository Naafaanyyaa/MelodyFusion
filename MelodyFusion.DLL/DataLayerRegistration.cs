using MelodyFusion.DLL.Entities.Identity;
using MelodyFusion.DLL.Interfaces;
using MelodyFusion.DLL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MelodyFusion.DLL
{
    public static class DataLayerRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserDto, RoleDto>(options =>
                {
                    options.Password.RequiredLength = 10;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddUserStore<UserStore<UserDto, RoleDto, ApplicationDbContext, string, IdentityUserClaim<string>, UserRole,
                    IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
                .AddRoleStore<RoleStore<RoleDto, ApplicationDbContext, string, UserRole, IdentityRoleClaim<string>>>()
                .AddSignInManager<SignInManager<UserDto>>()
                .AddRoleManager<RoleManager<RoleDto>>()
                .AddUserManager<UserManager<UserDto>>()
                .AddDefaultTokenProviders(); // Добавьте эту строку для регистрации стандартных провайдеров токенов;

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IAuthenticationStatisticRepository, AuthenticationStatisticRepository>();

            return services;
        }
    }
}
