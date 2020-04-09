using Microsoft.Extensions.DependencyInjection;
using Syngenta.RPA.Sintegra.Application.InputFiles;
using System;

namespace Syngenta.RPA.Sintegra.Bootstrapper
{
    public static class ApplicationsInitializer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IInputFilesApplication, InputFilesApplication>();

            return services;
        }
    }
}
