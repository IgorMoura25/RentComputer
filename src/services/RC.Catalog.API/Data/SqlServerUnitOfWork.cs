using RC.Core.Data;
using RC.Core.Mediator;

namespace RC.Catalog.API.Data
{
    public sealed class SqlServerUnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;
        private readonly MediatREventList _eventList;

        public SqlServerUnitOfWork(IDbSession session, MediatREventList eventList)
        {
            _session = session;
            _eventList = eventList;
        }

        public async Task<bool> BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();

            return true;
        }

        public async Task<bool> Commit()
        {
            _session.Transaction.Commit();
            Dispose();

            await _eventList.PublishEventsAsync();

            return true;
        }

        public async Task<bool> Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();

            return true;
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
