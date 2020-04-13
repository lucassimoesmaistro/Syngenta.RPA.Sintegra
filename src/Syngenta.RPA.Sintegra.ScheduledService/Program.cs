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
            if (args.Length == 0)
            {
                Console.WriteLine($"**********Invalid Command**********");
                return;
            }

            Console.WriteLine($"Arguments: {args[0]}");
            
            if (args[0].Equals("InputFiles"))
            {
                var application = serviceProvider.GetService<IInputFilesApplication>();
                var result = application.GetAllFilesInInputFolder().Result;
                result.ForEach(f => Console.WriteLine(f));
                
            }
            else if (args[0].Equals("SintegraComunication"))
            {

            }
            else if (args[0].Equals("CustomerUpdate"))
            {

            }
            else if (args[0].Equals("OutputFiles"))
            {

            }


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
