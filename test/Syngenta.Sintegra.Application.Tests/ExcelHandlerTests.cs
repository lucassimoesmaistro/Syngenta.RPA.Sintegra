using Moq;
using Syngenta.Common.Office;
using Syngenta.Sintegra.Application.InputFiles;
using Syngenta.Sintegra.Application.InputFiles.Models;
using Syngenta.Sintegra.Domain;
using System.Collections.Generic;
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
        }

        [Fact(DisplayName = "Read Excel File")]
        [Trait("Category", "Excel - Read Excel File")]
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
        [Trait("Category", "Excel - Import Data")]
        public void ShouldGetFilesToImport()
        {
            // Arrange  

            InputFilesApplication app = GetApplication();

            // Act
            List<string> filesList = app.GetAllFilesInInputFolder().Result;

            // Assert
            Assert.Single(filesList);
            Assert.Contains("CustomesDataTests.xlsx", filesList[0]);
        }

        private InputFilesApplication GetApplication()
        {
            var configuration = _applicationTestsFixture.GetConfiguration();

            var app = new InputFilesApplication(configuration, _applicationTestsFixture.AutoMapperInitializer());
            return app;
        }

        [Fact(DisplayName = "Import Customers from Excel Files")]
        [Trait("Category", "Excel - Import Data")]
        public void ShouldImportCurstomersFromExcelFiles()
        {
            // Arrange            
            InputFilesApplication app = GetApplication();

            // Act
            List<Request> requests = app.ImportCurstomersFromExcelFiles().Result;

            // Assert
            Assert.Single(requests);
            Assert.Equal(RequestStatus.Draft, requests.FirstOrDefault().RequestStatus);
            Assert.Equal(14244, requests.FirstOrDefault().RequestItems.Count);
        }

        
    }
}
