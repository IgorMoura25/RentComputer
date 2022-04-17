using RC.Core.Data.Dapper;

namespace RC.Catalog.API.Data.DTO
{
    public class AddProductDTO : AddProcedureDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
