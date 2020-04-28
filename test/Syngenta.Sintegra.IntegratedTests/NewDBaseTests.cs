using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Sintegra.AntiCorruption;
using Syngenta.Sintegra.Bootstrapper;
using System.IO;
using System.Linq;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{
    [Collection(nameof(IntegratedTestsCollection))]
    public class NewDBaseTests
    {
        private readonly IConfiguration _configuration;
        private readonly IntegratedTestsFixture _fixture;

        public NewDBaseTests(IntegratedTestsFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.BuildServiceCollection();
        }

        [Fact(DisplayName = "Get Data By Cnpj")]
        [Trait("Integrated Tests", "NewdBase")]
        public void ShouldGetDataByCnpj()
        {
            // Arrange
            var newDBase = new NewDBaseGateway(new ConfigurationManager(_configuration));

            // Act
            SintegraNacionalResponseDTO result = newDBase.GetDataByCnpj("91032201000126", "RS").Result;


            // Assert
            Assert.Equal(200, result.Response.Status.Code);
            Assert.NotNull(result);
            Assert.Equal("RS", result.Response.Output.FirstOrDefault().sg_UFLocalizacao);
            var cnpj = result.Response.Output.FirstOrDefault().nr_CNPJ.ToString().Replace(".", "");
            cnpj = cnpj.Replace("-", "");
            cnpj = cnpj.Replace("/", "");
            Assert.Equal("91032201000126", cnpj);
        }
        [Fact(DisplayName = "Get Data By CPF")]
        [Trait("Integrated Tests", "NewdBase")]
        public void ShouldGetDataByCpf()
        {
            // Arrange            

            var newDBase = new NewDBaseGateway(new ConfigurationManager(_configuration));

            // Act
            SintegraNacionalResponseDTO result = newDBase.GetDataByCpf("05348013072", "MT").Result;

            // Assert
            Assert.Equal(200, result.Response.Status.Code);
            Assert.NotNull(result);
            Assert.Equal("MT", result.Response.Output.FirstOrDefault().sg_UFLocalizacao);
        }
    }
}
