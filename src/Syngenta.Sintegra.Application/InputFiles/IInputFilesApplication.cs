using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Application.InputFiles
{
    public interface IInputFilesApplication : IDisposable
    {
        Task<List<string>> GetAllFilesInInputFolder();
        Task<List<Request>> ImportCurstomersFromExcelFiles();
    }
}
