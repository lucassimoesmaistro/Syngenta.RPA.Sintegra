using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Bootstrapper;
using Syngenta.Sintegra.Domain;
using Syngenta.Sintegra.Repository;
using Syngenta.Sintegra.Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{

    [Collection(nameof(IntegratedTestsCollection))]
    public class RepositoryTests
    {
        private readonly IConfiguration _configuration;
        private readonly IntegratedTestsFixture _fixture;

        public RepositoryTests(IntegratedTestsFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.BuildServiceCollection();
        }

        [Fact(DisplayName = "Get Validation Request With Registered Items")]
        [Trait("Integrated Tests", "Repository")]
        public void ShouldGetValidationRequestWithRegisteredItems()
        {
            // Arrange      
            var repository = new RequestRepository(_fixture.GetCustomerDbContext());

            List<Request> requests = repository.GetAllRequestsWithRegisteredItemsAndCommunicationFailure().Result.ToList();

            // Assert
            Assert.Equal(RequestStatus.RegisteredItems, requests.FirstOrDefault().RequestStatus);
            Assert.NotNull(requests.FirstOrDefault().RequestItems);
            Assert.NotEqual(0, requests.FirstOrDefault().RequestItems.Count);

        }
    }
}
