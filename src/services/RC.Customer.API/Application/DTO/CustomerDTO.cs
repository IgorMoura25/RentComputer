using RC.Customer.API.Domain;

namespace RC.Customer.API.Application.DTO
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public static CustomerDTO ToCustomerDTO(BusinessCustomer customer)
        {
            if (customer == null) return null;

            return new CustomerDTO
            {
                Name = customer.Name,
                Email = customer.Email?.EmailAddress,
                NationalId = customer.NationalId?.Number,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
