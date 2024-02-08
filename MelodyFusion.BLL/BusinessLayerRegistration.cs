using MelodyFusion.BLL.Infrastructure;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Services;
using MelodyFusion.DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MelodyFusion.BLL
{
    public static class BusinessLayerRegistration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IRoleInitializer, RoleInitializer>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddTransient<IBraintreeService, BraintreeService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<JwtHandler>();

            return services;
        }
    }
}
