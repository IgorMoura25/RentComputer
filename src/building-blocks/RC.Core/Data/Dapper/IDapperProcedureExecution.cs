namespace RC.Core.Data.Dapper
{
    public interface IDapperProcedureExecution : IDisposable
    {
        public IEnumerable<T1> ExecuteListProcedure<T1>(string procedureName, ListProcedureDTO procedureParameter, int? commandTimeout = null);
        public IEnumerable<T1> ExecuteListProcedure<T1, T2>(string procedureName, ListProcedureDTO procedureParameter, Func<T1, T2, T1> mapping, string splitOn, int? commandTimeout = null);
        public IEnumerable<T1> ExecuteListProcedure<T1, T2, T3>(string procedureName, ListProcedureDTO procedureParameter, Func<T1, T2, T3, T1> mapping, string splitOn, int? commandTimeout = null);

        public T1 ExecuteGetProcedure<T1, T2>(string procedureName, T2? procedureParameter = null, int? commandTimeout = null) where T2 : class;
        public T ExecuteGetProcedure<T>(string procedureName, int? commandTimeout = null);
        public T1 ExecuteGetProcedure<T1, T2, T3>(string procedureName, Func<T1, T3, T1> mapping, string splitOn, T2 procedureParameter, int? commandTimeout = null);
        public T1 ExecuteGetProcedure<T1, T2, T3, T4>(string procedureName, Func<T1, T3, T4, T1> mapping, string splitOn, T2 procedureParameter, int? commandTimeout = null);

        public T1 ExecuteAddProcedure<T1>(string procedureName, AddProcedureDTO procedureParameter, int? commandTimeout = null);
        public T1 ExecuteAddProcedure<T1, T2>(string procedureName, AddProcedureDTO procedureParameter, Func<T1, T2, T1> mapping, string splitOn, int? commandTimeout = null);
        public T1 ExecuteAddProcedure<T1, T2, T3>(string procedureName, AddProcedureDTO procedureParameter, Func<T1, T2, T3, T1> mapping, string splitOn, int? commandTimeout = null);
    }
}
