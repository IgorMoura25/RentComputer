using RC.Catalog.API.Data.DTO;
using RC.Catalog.API.Domain;
using RC.Core.Data.Dapper;

namespace RC.Catalog.API.Data.Repositories
{
    public class ProductCommandRepository : DapperRepository, IProductCommandRepository
    {
        private IDapperProcedureExecution _dapperProcedureExecution { get; }

        public ProductCommandRepository(IDapperProcedureExecution dapperProcedureExecution)
        {
            _dapperProcedureExecution = dapperProcedureExecution;
        }

        public Product Add(Product product)
        {
            return _dapperProcedureExecution.ExecuteAddProcedure<Product>
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
