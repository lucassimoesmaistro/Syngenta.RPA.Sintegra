using Microsoft.Extensions.Configuration;
using Syngenta.Sintegra.AntiCorruption;
using System;
using System.IO;
using Xunit;

namespace Syngenta.Sintegra.IntegratedTests
{
    public class NewDBaseTests
    {
        [Fact(DisplayName = "Get Data")]
        [Trait("Category", "NewdBase")]
        public void ShouldGetData()
        {
            // Arrange

            string appSetting = Directory.GetCurrentDirectory() + @"\appsettings.Test.json";
            var configuration =  new ConfigurationBuilder()
                        .AddJsonFile(appSetting)
                        .Build();

            var newDBase = new NewDBaseGateway(new ConfigurationManager(configuration));

            // Act
            var result = newDBase.GetDataByCnpj("91032201000126", "RS");

            // Assert
            Assert.Equal(200, result.Response.Status.Code);

        }
    }
}
