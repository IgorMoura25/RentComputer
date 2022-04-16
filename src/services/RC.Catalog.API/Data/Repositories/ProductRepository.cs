using RC.Catalog.API.Data.Dapper;
using RC.Catalog.API.Data.DTO;
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

        public Product GetByName(string name)
        {
            return _dapperProcedureExecution.ExecuteGetProcedure<Product, GetProductByNameDTO>("RC_GET_ProductByName", new GetProductByNameDTO() { Name = name }, CommandTimeout);
        }

        public Product Add(Product product)
        {
            return _dapperProcedureExecution
                .ExecuteAddProcedure<Product>
                (
                    "RC_ADD_Product",
                    new AddProductDTO()
                    {
                        UniversalId = product.UniversalId,
                        Name = product.Name,
                        Description = product.Description,
                        Value = product.Value,
                        Quantity = product.Quantity,
                        IsActive = product.IsActive,
                        CreatedAt = product.CreatedAt
                    },
                    CommandTimeout
                );
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
