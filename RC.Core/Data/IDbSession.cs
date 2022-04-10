using System.Data;

namespace RC.Core.Data
{
    public interface IDbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
    }
}
