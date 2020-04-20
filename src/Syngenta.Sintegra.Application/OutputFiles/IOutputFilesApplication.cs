using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Application.OutputFiles
{
    public interface IOutputFilesApplication : IDisposable
    {
        Task<bool> GetAllProcessed();
    }
}
