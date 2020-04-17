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
using System.Linq;
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
                          .Returns(Task.Run(() => GetRequestCollectionMock()));

            var requestMock = Task.Run(() => GetRequestCollectionMock()).Result;

            var customerCnpj = requestMock
                                            .ToList()
                                            .FirstOrDefault()
                                            .RequestItems.Where(w => w.CustomerId.Equals("10433438")).FirstOrDefault();

            var customerCpf = requestMock
                                            .ToList()
                                            .FirstOrDefault()
                                            .RequestItems.Where(w => w.CustomerId.Equals("10433438")).FirstOrDefault();

            var sintegraWebService = new Mock<ISintegraFacade>();

            sintegraWebService.Setup(x => x.GetDataByCnpj(customerCnpj.CustomerCNPJ, customerCnpj.CustomerRegion))
                          .Returns(Task.Run(() => GetCustomerMockCnpj()));

            sintegraWebService.Setup(x => x.GetDataByCpf(customerCnpj.CustomerCNPJ, customerCnpj.CustomerRegion))
                          .Returns(Task.Run(() => GetCustomerMockCpf()));

            var app = new DataValidatorApplication(AutoMapperInitializer(), repository.Object);

            return app;
        }

        private async Task<Customer> GetCustomerMockCpf()
        {
            return await Task.Run(() =>
                new Customer("ORLANDO POLATO E OUTRO", "BR 364 KM MAIS 118", "S/N", "ZN RURAL", "78795-000", "PEDRA PRETA", "BR", "MT", string.Empty, "14321092949", "132839717")
            );
        }

        private async Task<Customer> GetCustomerMockCnpj()
        {
            return await Task.Run(() =>
                new Customer("HEINZ BRASIL SA", "RDV GO 080", "S/N", "ZN RURAL", "75460-000", "NEROPOLIS", "BR", "GO", "50955707000472", string.Empty, "101884427")
            );
        }

        private async Task<IEnumerable<Request>> GetRequestCollectionMock()
        {
            return await Task.Run(() =>
            {
                var requestMock = new Request("FileTest.xlsx");
                requestMock.SetAsRegisteredItems();
                var item1 = new RequestItem("10433385", "PRODUTECNICA COM E REPRES LTDA", "R ALVARES CABRAL", "381", "PETROPOLIS", "99050-070", "PASSO FUNDO", "BR", "RS", "91032201000126", string.Empty, "0910103577");
                var item2 = new RequestItem("10433481", "ORLANDO POLATO E OUTRO", "BR 364 KM MAIS 118", "S/N", "ZN RURAL", "78795-000", "PEDRA PRETA", "BR", "MT", string.Empty, "14321092949", "132839717");
                var item3 = new RequestItem("10433438", "HEINZ BRASIL SA", "RDV GO 080", "S/N", "ZN RURAL", "75460-000", "NEROPOLIS", "BR", "GO", "50955707000472", string.Empty, "101884427");
                requestMock.AddItem(item1);
                requestMock.AddItem(item2);
                requestMock.AddItem(item3);
                var list = new List<Request>();
                list.Add(requestMock);
                return list;

            });
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
