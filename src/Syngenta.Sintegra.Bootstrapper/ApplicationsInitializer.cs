using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.OutputFiles;
using Syngenta.Sintegra.Application.SintegraComunication;
using System;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class ApplicationsInitializer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IInputFilesApplication, InputFilesApplication>();
            services.AddTransient<IDataValidatorApplication, DataValidatorApplication>();
            services.AddTransient<IOutputFilesApplication, OutputFilesApplication>();
            return services;
        }
    }
}
