using Syngenta.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Domain
{
    public interface IRequestRepository : IRepository<Request>
    {

        void Add(Request request);
        Task<IEnumerable<Request>> GetAllRequestsWithRegisteredItemsAndCommunicationFailure();
        void AddChangeLog(ChangeLog changeLog);
        void AtualizarItem(RequestItem item);
        void Update(Request request);
        Task<IEnumerable<Request>> GetAllRequestsWithAllItemsProcessed();
    }
}
