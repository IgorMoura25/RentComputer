using RC.Core.Data;

namespace RC.Catalog.API.Data
{
    public sealed class SqlServerUnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;

        public SqlServerUnitOfWork(IDbSession session)
        {
            _session = session;
        }

        public Task<bool> BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();

            return Task.FromResult(true);
        }

        public Task<bool> Commit()
        {
            _session.Transaction.Commit();
            Dispose();

            return Task.FromResult(true);
        }

        public Task<bool> Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();

            return Task.FromResult(true);
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
