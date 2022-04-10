namespace RC.Catalog.API.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        private IDapperProcedureExecution _dapperProcedureExecution { get; }

        public ProductRepository(IDapperProcedureExecution dapperProcedureExecution)
        {
            _dapperProcedureExecution = dapperProcedureExecution;
        }

        public IEnumerable<string> GetAll()
        {
            return _dapperProcedureExecution.ExecuteListProcedure<string>("EXE_TESTE", null, CommandTimeout);
        }

        public long Add(string model)
        {
            return _dapperProcedureExecution.ExecuteAddProcedure<string>("EXE_TESTE", null, CommandTimeout);
        }

        public void Dispose()
        {
            _dapperProcedureExecution?.Dispose();
        }
    }
}
