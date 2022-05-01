namespace RC.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool BeginTransaction();
        Task<bool> CommitAsync();
        Task<bool> RollbackAsync();
    }
}
