using Microsoft.Extensions.DependencyInjection;
using System;

namespace Syngenta.RPA.Sintegra.Bootstrapper
{
    public static class ApplicationsInitializer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddTransient<IAplicacaoDeVenda, AplicacaoDeVenda>();

            return services;
        }
    }
}
