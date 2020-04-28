using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syngenta.Common.Data;
using Syngenta.Common.Office;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.InputFiles.Models;
using Syngenta.Sintegra.Bootstrapper;
using Syngenta.Sintegra.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Syngenta.Sintegra.Application.Tests
{
    [Collection(nameof(ApplicationCollection))]
    public class ExcelHandlerTests
    {

        private readonly ApplicationTestsFixture _applicationTestsFixture;
        public ExcelHandlerTests(ApplicationTestsFixture applicationTestsFixture)
        {
            _applicationTestsFixture = applicationTestsFixture;
            _applicationTestsFixture.BuildServiceCollection();
        }

        [Fact(DisplayName = "Read Excel File")]
        [Trait("Unit Tests", "Excel - Read Excel File")]
        public void ShouldReadExcelFile()
        {       
            // Arrange            
            string excelPath = _applicationTestsFixture.GetConfiguration("FilesPath:NewFiles");
            string fileName = "CustomesDataTests.xlsx";
            // Act
            var excel = ExcelExtensions.Read<CustomersColletionModel>($@"{excelPath}\{fileName}");

            // Assert
            Assert.Equal(14244, excel.Count);
        }

        [Fact(DisplayName = "Get Files To Import")]
        [Trait("Unit Tests", "Excel - Import Data")]
        public void ShouldGetFilesToImport()
        {
            // Arrange  
            InputFilesApplication app = _applicationTestsFixture.GetInputFilesApplication();

            // Act
            List<string> filesList = app.GetAllFilesInInputFolder().Result;

            // Assert
            Assert.Single(filesList);
            Assert.Contains("CustomesDataTests.xlsx", filesList[0]);
        }

        [Fact(DisplayName = "Import Customers from Excel Files")]
        [Trait("Unit Tests", "Excel - Import Data")]
        public void ShouldImportCurstomersFromExcelFiles()
        {
            // Arrange    
            InputFilesApplication app = _applicationTestsFixture.GetInputFilesApplication();

            // Act
            List<Request> requests = app.ImportCurstomersFromExcelFiles().Result;

            // Assert
            Assert.Single(requests);
            Assert.Equal(RequestStatus.RegisteredItems, requests.FirstOrDefault().RequestStatus);
            Assert.Equal(14244, requests.FirstOrDefault().RequestItems.Count);
        }

        
    }
}
