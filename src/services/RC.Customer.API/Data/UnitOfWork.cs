using RC.Core.Data;
using RC.MessageBus.Mediator;

namespace RC.Customer.API.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;
        private readonly MediatREventList _eventList;

        public UnitOfWork(IDbSession session, MediatREventList eventList)
        {
            _session = session;
            _eventList = eventList;
        }

        public bool BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();

            return true;
        }

        public async Task<bool> CommitAsync()
        {
            _session.Transaction.Commit();
            Dispose();

            await _eventList.PublishEventsAsync();

            return true;
        }

        public async Task<bool> RollbackAsync()
        {
            _session.Transaction.Rollback();
            Dispose();

            return true;
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
