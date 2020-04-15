using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Syngenta.Sintegra.Application.AutoMapper;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.SintegraComunication;
using Syngenta.Sintegra.Domain;
using Syngenta.Sintegra.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public DataValidatorApplication GetDataValidatorApplication()
        {
            var configuration = GetConfiguration();

            var repository = new Mock<IRequestRepository>();
            
            repository.Setup(x => x.GetAllRequestsWithRegisteredItems())
                          .Returns(Task.Run(()=>GetRequestCollectionMock()));

            var app = new DataValidatorApplication(configuration, AutoMapperInitializer(), repository.Object);

            return app;
        }

        private async Task<IEnumerable<Request>> GetRequestCollectionMock()
        {
            var requestMock = new Request("FileTest.xlsx");
            requestMock.SetAsRegisteredItems();
            var item1 = new RequestItem("test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test");
            var item2 = new RequestItem("test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test");
            requestMock.AddItem(item1);
            requestMock.AddItem(item2);
            var list = new List<Request>();
            list.Add(requestMock);
            return list;
        }

        public string GetConfiguration(string configurationKey)
        {
            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            return configuration[configurationKey];
        }

        public InputFilesApplication GetInputFilesApplication()
        {
            var configuration = GetConfiguration();

            var repository = new Mock<IRequestRepository>();

            var app = new InputFilesApplication(configuration, AutoMapperInitializer(), repository.Object);

            return app;
        }
        public IMapper AutoMapperInitializer()
        {
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
