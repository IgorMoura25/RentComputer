using RC.Core.Data;
using System.Data;
using System.Data.SqlClient;

namespace RC.Catalog.API.Data
{
    public sealed class SqlServerDbSession : IDbSession, IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public SqlServerDbSession(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
