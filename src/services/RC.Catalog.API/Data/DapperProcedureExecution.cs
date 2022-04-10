using Dapper;
using RC.Core.Data;

namespace RC.Catalog.API.Data
{
    public class DapperProcedureExecution : IDapperProcedureExecution
    {
        private IDbSession _session;

        public DapperProcedureExecution(IDbSession session)
        {
            _session = session;
        }

        public IEnumerable<T> ExecuteListProcedure<T>(string procedureName, T? procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query<T>(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    transaction: _session.Transaction);

            return procedureResult?.ToList();
        }

        public long ExecuteAddProcedure<T>(string procedureName, T? procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.QuerySingle<long>(
                procedureName,
                param: procedureParameter,
                commandType: System.Data.CommandType.StoredProcedure,
                commandTimeout: commandTimeout,
                transaction: _session.Transaction);

            return procedureResult;
        }

        public void Dispose()
        {
            _session?.Transaction?.Dispose();
        }
    }
}
