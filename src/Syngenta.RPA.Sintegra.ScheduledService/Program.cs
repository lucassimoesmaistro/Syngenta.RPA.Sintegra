using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.RPA.Sintegra.Application.InputFiles;
using Syngenta.RPA.Sintegra.Bootstrapper;
using System;
using System.Threading.Tasks;

namespace Syngenta.RPA.Sintegra.ScheduledService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceProvider serviceProvider = Startup(args);

            var application = serviceProvider.GetService<IInputFilesApplication>();
            application.GetAllFilesInInputFolder();
            //if (args[1].Equals("InputFiles"))
            //{
            //    var application = serviceProvider.GetService<IInputFilesApplication>();
            //    application.GetAllFilesInInputFolder();
            //}
            //else if (args[1].Equals("SintegraComunication"))
            //{

            //}
            //else if (args[1].Equals("CustomerUpdate"))
            //{

            //}
            //else if (args[1].Equals("OutputFiles"))
            //{

            //}


        }

        private static ServiceProvider Startup(string[] args)
        {
            string environment = args.Length == 0 ? "Production" : args[0];

            string appsettings = "";

            if (environment.Equals("Production"))
                appsettings = "appsettings.json";
            else
                appsettings = "appsettings.Development.json";

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("\\" + System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name, "");

            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(appsettings, optional: true, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
               .AddSingleton(_ => config)
               .AddSingleton<IConfiguration>(_ => config)
               .AddRepository()
               .AddAntiCorruptionLayer()
               .AddDomain()
               .AddApplication()
               .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
