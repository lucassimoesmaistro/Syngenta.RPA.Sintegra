using Microsoft.Extensions.Configuration;
using Syngenta.Sintegra.AntiCorruption;
using Syngenta.Sintegra.AntiCorruption.DTO;
using System;
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
            Output result = newDBase.GetDataByCnpj("91032201000126", "RS").Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RS", result.sg_UFLocalizacao);
            var cnpj = result.nr_CNPJ.ToString().Replace(".", "");
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
            var result = newDBase.GetDataByCpf("14321092949", "MT").Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("MT", result.sg_UFLocalizacao);
        }
    }
}
