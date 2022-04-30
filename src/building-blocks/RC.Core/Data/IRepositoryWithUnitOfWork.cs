using RC.Core.DomainObjects;

namespace RC.Core.Data
{
    public interface IRepositoryWithUnitOfWork<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
