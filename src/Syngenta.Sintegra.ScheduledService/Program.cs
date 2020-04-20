using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.Common.Log;
using Syngenta.Sintegra.Application.AutoMapper;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.SintegraComunication;
using Syngenta.Sintegra.Bootstrapper;
using Syngenta.Sintegra.Repository;
using System;

namespace Syngenta.Sintegra.ScheduledService
{
    //dotnet publish -r win10-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
    //Syngenta.Sintegra.ScheduledService.exe Development InputFilesApplication
    //Syngenta.Sintegra.ScheduledService.exe Development DataValidatorApplication
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine($"**********Invalid Command**********");
                Console.ReadKey();
                return;
            }
            ServiceProvider serviceProvider = Startup(args);

            Logger.Logar.Information($"Arguments: {args[1]}");

            if (args[1].Equals("InputFilesApplication"))
            {
                Logger.Logar.Information("Initializing Input Files Process");
                var application = serviceProvider.GetService<IInputFilesApplication>();
                var result = application.ImportCurstomersFromExcelFiles().Result;
                result.ForEach(f => Logger.Logar.Information(f.FileName));
                
            }
            else if (args[1].Equals("DataValidatorApplication"))
            {
                Logger.Logar.Information("Initializing Sintegra Comunication");
                var application = serviceProvider.GetService<IDataValidatorApplication>();
                var result = application.GetAllNewRequestsAndVerify().Result;
                Logger.Logar.Information(result ? "Completo" : "Incompleto");
            }
            else if (args[1].Equals("CustomerUpdate"))
            {
                Logger.Logar.Information("Initializing Customer Update");

            }
            else if (args[1].Equals("OutputFilesApplication"))
            {
                Logger.Logar.Information("Initializing Output Files Process");

            }


        }

        private static ServiceProvider Startup(string[] args)
        {
            string environment = args.Length == 1 ? "Production" : args[0];

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
               .AddAutoMapper(typeof(ViewModelToDomainMappingProfile),
                              typeof(DataTransferObjectToDomainMappingProfile))
               .AddSingleton(_ => config)
               .AddSingleton<IConfiguration>(_ => config)
               .AddDbContext<CustomerContext>(options =>
                                options.UseSqlServer(
                                    config.GetConnectionString("DefaultConnection")))
               .AddRepository()
               .AddAntiCorruptionLayer()
               .AddDomain()
               .AddApplication()
               .AddLog($"Syngenta.Sintegra.ScheduledService.{args[1].ToString()}", config["FilesPath:LogFiles"])
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
