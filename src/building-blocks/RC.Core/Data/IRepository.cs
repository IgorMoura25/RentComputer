using RC.Core.DomainObjects;

namespace RC.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
