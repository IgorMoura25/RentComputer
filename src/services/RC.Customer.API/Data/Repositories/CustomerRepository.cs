using RC.Core.Data.Dapper;
using RC.Core.DomainObjects;
using RC.Customer.API.Data.DTO;
using RC.Customer.API.Domain;

namespace RC.Customer.API.Data.Repositories
{
    public class CustomerRepository : DapperRepository, ICustomerRepository
    {
        private IDapperProcedureExecution _dapperProcedureExecution { get; }

        public CustomerRepository(IDapperProcedureExecution dapperProcedureExecution)
        {
            _dapperProcedureExecution = dapperProcedureExecution;
        }

        public IEnumerable<BusinessCustomer> GetAll()
        {
            return _dapperProcedureExecution.ExecuteListProcedure<BusinessCustomer, Email, NationalId>(
                "RC_LST_Customers",
                new ListProcedureDTO() { Offset = 0, Count = long.MaxValue },
                (customer, email, nationalId) =>
                {
                    customer.SetEmail(email);
                    customer.SetNationalId(nationalId);

                    return customer;
                },
                "EmailAddress, Number",
                CommandTimeout);
        }

        public BusinessCustomer GetByNationalId(string nationalId)
        {
            return _dapperProcedureExecution.ExecuteGetProcedure<BusinessCustomer, GetCustomerByNationalIdDTO, Email, NationalId>(
                "RC_GET_CustomerByNationalId",
                (customer, email, nationalId) =>
                {
                    customer.SetEmail(email);
                    customer.SetNationalId(nationalId);

                    return customer;
                },
                "EmailAddress, Number",
                new GetCustomerByNationalIdDTO()
                {
                    NationalId = nationalId
                },
                CommandTimeout);
        }

        public BusinessCustomer Add(BusinessCustomer customer)
        {
            return _dapperProcedureExecution.ExecuteAddProcedure<BusinessCustomer, Email, NationalId>(
                    "RC_ADD_Customer",
                    new AddCustomerDTO()
                    {
                        UniversalId = customer.UniversalId,
                        Name = customer.Name,
                        Email = customer.Email?.EmailAddress,
                        NationalId = customer.NationalId?.Number,
                        IsActive = customer.IsActive,
                        CreatedAt = customer.CreatedAt
                    },
                    (customer, email, nationalId) =>
                    {
                        customer.SetEmail(email);
                        customer.SetNationalId(nationalId);

                        return customer;
                    },
                    "EmailAddress, Number",
                    CommandTimeout);
        }

        public void Dispose()
        {
            _dapperProcedureExecution?.Dispose();
        }

        public override void AddCustomTypeHandlersBaseCall()
        {
        }
    }
}
