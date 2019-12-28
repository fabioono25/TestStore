using System;
using TestStore.Core.DomainObjects;

namespace TestStore.Core.Data
{
    public interface IRepository<T>: IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get;  }
    }
}
