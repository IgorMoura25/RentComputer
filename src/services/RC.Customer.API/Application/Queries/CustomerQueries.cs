using RC.Customer.API.Application.DTO;
using RC.Customer.API.Data.Repositories;

namespace RC.Customer.API.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueries(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var customerList = _customerRepository.GetAll();

            return customerList.Select(CustomerDTO.ToCustomerDTO);
        }
    }
}
