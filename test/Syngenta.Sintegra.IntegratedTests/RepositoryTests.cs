using Syngenta.Sintegra.Domain;
using Syngenta.Sintegra.Repository;
using Syngenta.Sintegra.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{

    [Collection(nameof(RepositoryCollection))]
    public class RepositoryTests
    {
        private readonly RepositoryTestsFixture _fixture;

        public RepositoryTests(RepositoryTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Get Validation Request With Registered Items")]
        [Trait("Category", "Repository")]
        public void ShouldGetValidationRequestWithRegisteredItems()
        {
            // Arrange            

            var repository = new RequestRepository(_fixture.GetCustomerDbContext());

            List<Request> requests = repository.GetAllRequestsWithRegisteredItems().Result.ToList();

            // Assert
            Assert.Equal(RequestStatus.RegisteredItems, requests.FirstOrDefault().RequestStatus);
            Assert.NotNull(requests.FirstOrDefault().RequestItems);
            Assert.NotEqual(0, requests.FirstOrDefault().RequestItems.Count);

        }
    }
}
