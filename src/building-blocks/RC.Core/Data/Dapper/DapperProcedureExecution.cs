using Dapper;

namespace RC.Core.Data.Dapper
{
    public class DapperProcedureExecution : IDapperProcedureExecution
    {
        private IDbSession _session;

        public DapperProcedureExecution(IDbSession session)
        {
            _session = session;
        }

        #region List
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

        public IEnumerable<T1> ExecuteListProcedure<T1, T2>(string procedureName, ListProcedureDTO procedureParameter, Func<T1, T2, T1> mapping, string splitOn, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult;
        }

        public IEnumerable<T1> ExecuteListProcedure<T1, T2, T3>(string procedureName, ListProcedureDTO procedureParameter, Func<T1, T2, T3, T1> mapping, string splitOn, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult;
        }

        #endregion

        #region Add
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
        public T1 ExecuteAddProcedure<T1, T2>(string procedureName, AddProcedureDTO procedureParameter, Func<T1, T2, T1> mapping, string splitOn, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult.FirstOrDefault();
        }
        public T1 ExecuteAddProcedure<T1, T2, T3>(string procedureName, AddProcedureDTO procedureParameter, Func<T1, T2, T3, T1> mapping, string splitOn, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult.FirstOrDefault();
        }
        #endregion

        #region Get
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

        public T1 ExecuteGetProcedure<T1, T2, T3>(string procedureName, Func<T1, T3, T1> mapping, string splitOn, T2 procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult.FirstOrDefault();
        }

        public T1 ExecuteGetProcedure<T1, T2, T3, T4>(string procedureName, Func<T1, T3, T4, T1> mapping, string splitOn, T2 procedureParameter, int? commandTimeout = null)
        {
            var procedureResult = _session.Connection.Query(
                    procedureName,
                    param: procedureParameter,
                    commandType: System.Data.CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                    map: mapping,
                    splitOn: splitOn,
                    transaction: _session.Transaction);

            return procedureResult.FirstOrDefault();
        }
        #endregion

        public void Dispose()
        {
            _session?.Transaction?.Dispose();
        }
    }
}
