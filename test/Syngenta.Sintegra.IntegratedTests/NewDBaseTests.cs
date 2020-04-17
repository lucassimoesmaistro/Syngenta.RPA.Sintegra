using Microsoft.Extensions.Configuration;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Sintegra.AntiCorruption;
using System.IO;
using System.Linq;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{
    public class NewDBaseTests
    {
        [Fact(DisplayName = "Get Data By Cnpj")]
        [Trait("Integrated Tests", "NewdBase")]
        public void ShouldGetDataByCnpj()
        {
            // Arrange

            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration =  new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            var newDBase = new NewDBaseGateway(new ConfigurationManager(configuration));

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

            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            var newDBase = new NewDBaseGateway(new ConfigurationManager(configuration));

            // Act
            SintegraNacionalResponseDTO result = newDBase.GetDataByCpf("14321092949", "MT").Result;

            // Assert
            Assert.Equal(200, result.Response.Status.Code);
            Assert.NotNull(result);
            Assert.Equal("MT", result.Response.Output.FirstOrDefault().sg_UFLocalizacao);
            var cpf = result.Response.Output.FirstOrDefault().nr_CNPJ.ToString().Replace(".", "");
            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace("/", "");
            Assert.Equal("14321092949", cpf);
        }
    }
}
