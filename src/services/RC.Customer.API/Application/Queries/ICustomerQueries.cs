using RC.Customer.API.Application.DTO;

namespace RC.Customer.API.Application.Queries
{
    public interface ICustomerQueries
    {
        IEnumerable<CustomerDTO> GetAll();
    }
}
