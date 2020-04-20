using Microsoft.Extensions.DependencyInjection;
using Syngenta.Common.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class LogServiceInitializer
    {
        public static Logger Logar { get; set; }
        public static IServiceCollection AddLog(this IServiceCollection services, string applicationName, string logFilesFolder)
        {
            Logar = new Logger(applicationName, logFilesFolder);

            return services.AddSingleton(Logar);
        }
    }
}
