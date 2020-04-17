using Moq;
using Moq.AutoMock;
using Syngenta.Sintegra.Application.SintegraComunication;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Syngenta.Sintegra.Application.Tests
{
    [Collection(nameof(ApplicationCollection))]
    public class DataValidatorTests
    {

        private readonly ApplicationTestsFixture _applicationTestsFixture;
        private readonly AutoMocker _mocker;
        private readonly DataValidatorApplication _dataValidatorApp;

        public DataValidatorTests(ApplicationTestsFixture applicationTestsFixture)
        {
            _applicationTestsFixture = applicationTestsFixture;
            _mocker = new AutoMocker();
            _dataValidatorApp = _mocker.CreateInstance<DataValidatorApplication>();
        }

        [Fact(DisplayName = "Get Validation Request With Registered Items")]
        [Trait("Unit Tests", "Sintegra")]
        public void ShouldGetValidationRequestWithRegisteredItems()
        {
            // Arrange            
            DataValidatorApplication app = _applicationTestsFixture.GetDataValidatorApplication();    

            // Act
            List<Request> requests = app.GetValidationRequestWithRegisteredItems().Result.ToList();

            // Assert
            Assert.Equal(RequestStatus.RegisteredItems, requests.FirstOrDefault().RequestStatus);
            Assert.NotNull(requests.FirstOrDefault().RequestItems);
            Assert.NotEqual(0, requests.FirstOrDefault().RequestItems.Count);
        }

        [Fact(DisplayName = "Get All Requests And Verify")]
        [Trait("Unit Tests", "Differences Between Excel Request And Sintegra Data")]
        public void ShouldGetAllRequestsAndVerify()
        {
            // Arrange        
            _mocker.GetMock<IRequestRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IRequestRepository>()
                .Setup(r => r.GetAllRequestsWithRegisteredItems()).Returns(_applicationTestsFixture.GetRequestCollectionMock());
            _mocker.GetMock<ISintegraFacade>()
                .Setup(r => r.GetDataByCnpj(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_applicationTestsFixture.GetCustomerMockCnpj());
            _mocker.GetMock<ISintegraFacade>()
                .Setup(r => r.GetDataByCpf(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_applicationTestsFixture.GetCustomerMockCpf());


            // Act
            var result = _dataValidatorApp.GetAllNewRequestsAndVerify().Result;


            // Assert
            Assert.True(result);
            _mocker.GetMock<IRequestRepository>().Verify(r => r.GetAllRequestsWithRegisteredItems(), Times.Once);
            _mocker.GetMock<ISintegraFacade>().Verify(r => r.GetDataByCnpj(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeast(2));
            _mocker.GetMock<ISintegraFacade>().Verify(r => r.GetDataByCpf(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IRequestRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Verify")]
        [Trait("Unit Tests", "Differences Between Excel Request And Sintegra Data")]
        public void ShouldVerifyDifferenceBetweenExcelRequestAndSintegraData()
        {
            // Arrange        
            DataValidatorApplication app = _applicationTestsFixture.GetDataValidatorApplication();
            var item = _applicationTestsFixture.GetRequestItemCnpjMock().Result;
            var customer = _applicationTestsFixture.GetCustomerMockCnpj().Result;

            _mocker.GetMock<IRequestRepository>().Setup(r => r.AddChangeLog(It.IsAny<ChangeLog>()));

            // Act
            _dataValidatorApp.VerifyDifferenceBetweenRequestItemAndSintegra(item, customer).Wait();

            // Assert
            _mocker.GetMock<IRequestRepository>().Verify(r => r.AddChangeLog(It.IsAny<ChangeLog>()), Times.Exactly(4));
        }

    }
}
