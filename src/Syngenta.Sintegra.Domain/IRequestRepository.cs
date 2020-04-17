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
        Task<IEnumerable<Request>> GetAllRequestsWithRegisteredItems();
        void AddChangeLog(ChangeLog changeLog);
    }
}
