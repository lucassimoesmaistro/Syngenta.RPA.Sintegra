using Microsoft.EntityFrameworkCore;
using Syngenta.Common.Data;
using Syngenta.Sintegra.Domain;
using Syngenta.Sintegra.Repository.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Repository.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly CustomerContext _db;

        public RequestRepository(CustomerContext context)
        {
            _db = context;
        }
        public IUnitOfWork UnitOfWork => _db;

        public void Add(Request request)
        {
            _db.RequestVerification.Add(request);
        }

        public void AddChangeLog(ChangeLog changeLog)
        {
            _db.ChangeLogs.Add(changeLog);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
        public async Task<IEnumerable<Request>> GetAllRequestsWithRegisteredItems()
        {
//            var sql = _db.RequestVerification.Where(w => w.RequestStatus.Equals(RequestStatus.RegisteredItems)).ToSql();
            return await _db.RequestVerification
                .Where(w => w.RequestStatus.Equals(RequestStatus.RegisteredItems))
                .Include(i=>i.RequestItems)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}