﻿namespace RC.Catalog.API.Data.Dapper
{
    public interface IDapperProcedureExecution : IDisposable
    {
        IEnumerable<T1> ExecuteListProcedure<T1>(string procedureName, ListProcedureDTO procedureParameter, int? commandTimeout = null);

        T1 ExecuteGetProcedure<T1, T2>(string procedureName, T2? procedureParameter = null, int? commandTimeout = null) where T2 : class;
        T ExecuteGetProcedure<T>(string procedureName, int? commandTimeout = null);

        T1 ExecuteAddProcedure<T1>(string procedureName, AddProcedureDTO procedureParameter, int? commandTimeout = null);
    }
}
