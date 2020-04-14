using AutoMapper;
using Microsoft.Extensions.Configuration;
using Syngenta.Sintegra.Application.AutoMapper;
using System;
using System.IO;
using Xunit;

namespace Syngenta.Sintegra.Application.Tests
{
    [CollectionDefinition(nameof(ApplicationCollection))]
    public class ApplicationCollection : ICollectionFixture<ApplicationTestsFixture>
    { }

    public class ApplicationTestsFixture : IDisposable
    {
        public IConfiguration GetConfiguration()
        {
            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            return new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();
        }
        public string GetConfiguration(string configurationKey)
        {
            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            return configuration[configurationKey];
        }
        public IMapper AutoMapperInitializer()
        {
            
            Mapper.Reset();
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
            var mockMapper = new MapperConfiguration(x =>
            {
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
        }
    }
}
