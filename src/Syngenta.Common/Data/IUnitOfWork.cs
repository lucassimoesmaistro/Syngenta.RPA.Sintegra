using System.Threading.Tasks;

namespace Syngenta.Common.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
