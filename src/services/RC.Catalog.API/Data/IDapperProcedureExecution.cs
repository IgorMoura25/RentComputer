namespace RC.Catalog.API.Data
{
    public interface IDapperProcedureExecution : IDisposable
    {
        IEnumerable<T> ExecuteListProcedure<T>(string procedureName, T? procedureParameter, int? commandTimeout = null);
        long ExecuteAddProcedure<T>(string procedureName, T? procedureParameter, int? commandTimeout = null);
    }
}
