using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Syngenta.RPA.Sintegra.Application.InputFiles
{
    public class InputFilesApplication : IInputFilesApplication
    {
        private readonly string _filesPath;
        public InputFilesApplication(IConfiguration configuration)
        {
            _filesPath = configuration["FilesPath:NewFiles"];
        }

        public IConfiguration Configuration { get; }

        public async Task<bool> GetAllFilesInInputFolder()
        {
            Console.WriteLine(DateTime.Now);
            Thread.Sleep(15000);
            Console.WriteLine(DateTime.Now);
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
