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

        public void AtualizarItem(RequestItem item)
        {
            _db.RequestItems.Update(item);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public async Task<IEnumerable<Request>> GetAllRequestsWithAllItemsProcessed()
        {
            return await _db.RequestVerification
                .Where(w => w.RequestStatus.Equals(RequestStatus.Processed))
                .Include(i => i.RequestItems)
                .ThenInclude(t => t.ChangeLogs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequestsWithRegisteredItemsAndCommunicationFailure()
        {
            //var sql = _db.RequestVerification.Where(w => w.RequestStatus.Equals(RequestStatus.RegisteredItems))
            //    .Include(i => i.RequestItems)
            //        .Include(i => i.RequestItemStatus == RequestItemStatus.Registered).ToSql();
            return await _db.RequestVerification
                .Where(w => w.RequestStatus.Equals(RequestStatus.RegisteredItems))
                .Include(i => i.RequestItems)
                    //.ThenInclude(i=>i.RequestItemStatus == RequestItemStatus.Registered)
                .AsNoTracking()
                .ToListAsync();


        }

        public void Update(Request request)
        {
            _db.RequestVerification.Update(request);
        }
    }
}