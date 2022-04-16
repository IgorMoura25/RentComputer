using Dapper;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Dapper
{
    public class DapperProcedureExecution : IDapperProcedureExecution
    {
        private IDbSession _session;

        public DapperProcedureExecution(IDbSession session)
        {
            _session = session;
        }

        public IEnumerable<T1> ExecuteListProcedure<T1>(string procedureName, ListProcedureDTO procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query<T1>(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    transaction: _session.Transaction);

            return procedureResult;
        }

        public T1 ExecuteGetProcedure<T1, T2>(string procedureName, T2? procedureParameter = null, int? commandTimeout = null) where T2 : class
        {
            var procedureResult = _session.Connection.QuerySingleOrDefault<T1>(
                procedureName,
                param: procedureParameter,
                commandType: System.Data.CommandType.StoredProcedure,
                commandTimeout: commandTimeout,
                transaction: _session.Transaction);

            return procedureResult;
        }

        public T ExecuteGetProcedure<T>(string procedureName, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.QuerySingleOrDefault<T>(
                procedureName,
                commandType: System.Data.CommandType.StoredProcedure,
                commandTimeout: commandTimeout,
                transaction: _session.Transaction);

            return procedureResult;
        }

        public T1 ExecuteAddProcedure<T1>(string procedureName, AddProcedureDTO procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.QuerySingle<T1>(
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
