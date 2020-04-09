using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.RPA.Sintegra.Application.InputFiles
{
    public interface IInputFilesApplication : IDisposable
    {
        Task<bool> GetAllFilesInInputFolder();
    }
}
