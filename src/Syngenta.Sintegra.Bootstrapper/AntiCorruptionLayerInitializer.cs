using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.AntiCorruption;
using Syngenta.Sintegra.Domain;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class AntiCorruptionLayerInitializer
    {

        public static IServiceCollection AddAntiCorruptionLayer(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IPartnerDataSourceGateway, NewDBaseGateway>();
            services.AddTransient<ISintegraFacade, SintegraFacade>();

            return services;
        }
    }
}
