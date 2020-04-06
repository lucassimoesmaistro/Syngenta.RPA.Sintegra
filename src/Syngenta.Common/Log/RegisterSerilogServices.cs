using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Syngenta.Common.Log
{
    public static class RegisterSerilogServices
    {
        public static Logger Logar { get; set; }
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, string applicationName)
        {
            Logar = new Logger(applicationName);

            return services.AddSingleton(Logar);
        }
    }
}
