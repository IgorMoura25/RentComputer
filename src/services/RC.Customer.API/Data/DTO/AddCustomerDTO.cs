using RC.Core.Data.Dapper;

namespace RC.Customer.API.Data.DTO
{
    public class AddCustomerDTO : AddProcedureDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
    }
}
