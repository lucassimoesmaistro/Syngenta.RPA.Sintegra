using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class DomainInitializer
    {

        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            //services.AddTransient<IServicoDeIntegracaoAbcfarma, ServicoDeIntegracaoAbcfarma>();

            return services;
        }
    }
}
