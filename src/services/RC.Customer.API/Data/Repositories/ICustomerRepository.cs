using RC.Core.Data;
using RC.Customer.API.Domain;

namespace RC.Customer.API.Data.Repositories
{
    public interface ICustomerRepository : IRepository<BusinessCustomer>
    {
        IEnumerable<BusinessCustomer> GetAll();
        BusinessCustomer GetByNationalId(string nationalId);
        BusinessCustomer Add(BusinessCustomer model);
    }
}
