using Syngenta.Common.Data;
using Syngenta.Sintegra.Domain;

namespace Syngenta.Sintegra.Repository.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly CustomerContext _context;

        public RequestRepository(CustomerContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Request request)
        {
            _context.RequestVerification.Add(request);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}