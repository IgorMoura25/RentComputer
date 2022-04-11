using Dapper;
using RC.Catalog.API.Data.Dapper;
using RC.Catalog.API.Domain;

namespace RC.Catalog.API.Data.Repositories
{
    public class ProductRepository : DapperRepository, IProductRepository
    {
        private IDapperProcedureExecution _dapperProcedureExecution { get; }

        public ProductRepository(IDapperProcedureExecution dapperProcedureExecution)
        {
            _dapperProcedureExecution = dapperProcedureExecution;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dapperProcedureExecution.ExecuteListProcedure<Product>("RC_LST_Products", new ListProcedureDTO() { Offset = 0, Count = long.MaxValue }, CommandTimeout);
        }

        public Product Add(string model)
        {
            return _dapperProcedureExecution.ExecuteAddProcedure<Product>("EXE_TESTE", CommandTimeout);
        }

        public void Dispose()
        {
            _dapperProcedureExecution?.Dispose();
        }

        public override void AddCustomTypeHandlersBaseCall()
        {
            base.AddCustomTypeHandler<IEnumerable<ProductImage>>();
        }
    }
}
