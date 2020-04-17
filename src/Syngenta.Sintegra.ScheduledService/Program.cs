using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Application.AutoMapper;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.SintegraComunication;
using Syngenta.Sintegra.Bootstrapper;
using Syngenta.Sintegra.Repository;
using System;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.ScheduledService
{
    //dotnet publish -r win10-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
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
                var result = application.ImportCurstomersFromExcelFiles().Result;
                result.ForEach(f => Console.WriteLine(f.FileName));
                
            }
            else if (args[0].Equals("SintegraComunication"))
            {
                var application = serviceProvider.GetService<IDataValidatorApplication>();
                var result = application.GetAllNewRequestsAndVerify().Result;
                Console.WriteLine(result ? "Completo" : "Incompleto");
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
               .AddAutoMapper(typeof(ViewModelToDomainMappingProfile))
               .AddSingleton(_ => config)
               .AddSingleton<IConfiguration>(_ => config)
               .AddDbContext<CustomerContext>(options =>
                                options.UseSqlServer(
                                    config.GetConnectionString("DefaultConnection")))
               .AddRepository()
               .AddAntiCorruptionLayer()
               .AddDomain()
               .AddApplication()
               .BuildServiceProvider();
            return serviceProvider;
        }
    }

    //Add-Migration Initial -Context CustomerContext -s Syngenta.Sintegra.ScheduledService
    //Update-database -Context CustomerContext -s Syngenta.Sintegra.ScheduledService –Verbose
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("\\" + System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name, "");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CustomerContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new CustomerContext(builder.Options);
        }
    }
}
