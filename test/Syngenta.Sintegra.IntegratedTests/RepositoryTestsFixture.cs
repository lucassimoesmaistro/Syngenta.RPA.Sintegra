using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{
    [CollectionDefinition(nameof(RepositoryCollection))]
    public class RepositoryCollection : ICollectionFixture<RepositoryTestsFixture>
    { }

    public class RepositoryTestsFixture : IDisposable
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


        public void Dispose()
        {
        }
    }
}
