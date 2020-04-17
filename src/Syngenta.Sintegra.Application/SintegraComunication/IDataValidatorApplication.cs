using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Application.SintegraComunication
{
    public interface IDataValidatorApplication : IDisposable
    {
        Task<IEnumerable<Request>> GetValidationRequestWithRegisteredItems();

        Task<bool> VerifyDifferenceBetweenRequestAndSintegra();
        Task<bool> VerifyAllRequestWithRegistredItems();
    }
}
