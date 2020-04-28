using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Bootstrapper;
using Syngenta.Sintegra.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{
    [CollectionDefinition(nameof(IntegratedTestsCollection))]
    public class IntegratedTestsCollection : ICollectionFixture<IntegratedTestsFixture>
    { }

    public class IntegratedTestsFixture : IDisposable
    {
        public string GetConfiguration(string configurationKey)
        {
            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            return configuration[configurationKey];
        }
        public IConfiguration GetConfiguration()
        {
            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            return new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();
        }

        public CustomerContext GetCustomerDbContext()
        {
            var config = GetConfiguration();
            
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
                    .UseSqlServer(config.GetConnectionString("DefaultConnection"));

            var dbContext = new CustomerContext(dbContextOptionsBuilder.Options);

            return dbContext;
        }

        public IConfiguration BuildServiceCollection()
        {

            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            var serviceProvider = new ServiceCollection()
               .AddLog($"Syngenta.Sintegra.ScheduledService.Tests", configuration["FilesPath:LogFiles"]);

            serviceProvider.BuildServiceProvider();
            return configuration;
        }

        public void Dispose()
        {
        }
    }
}
