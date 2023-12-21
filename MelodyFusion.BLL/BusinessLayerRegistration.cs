using MelodyFusion.DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MelodyFusion.BLL
{
    public static class BusinessLayerRegistration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

            return services;
        }
    }
}
