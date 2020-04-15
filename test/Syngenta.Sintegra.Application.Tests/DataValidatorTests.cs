using Syngenta.Sintegra.Application.SintegraComunication;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Syngenta.Sintegra.Application.Tests
{
    [Collection(nameof(ApplicationCollection))]
    public class DataValidatorTests
    {

            private readonly ApplicationTestsFixture _applicationTestsFixture;
            public DataValidatorTests(ApplicationTestsFixture applicationTestsFixture)
            {
                _applicationTestsFixture = applicationTestsFixture;
            }

            [Fact(DisplayName = "Get Validation Request With Registered Items")]
            [Trait("Category", "Sintegra")]
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
        }
}
