namespace RC.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> BeginTransaction();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}
