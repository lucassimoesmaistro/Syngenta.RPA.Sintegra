using Syngenta.Common.DomainObjects;
using System;

namespace Syngenta.Common.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
