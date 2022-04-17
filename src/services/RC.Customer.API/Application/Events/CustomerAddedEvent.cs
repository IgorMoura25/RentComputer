using RC.Core.Messages;

namespace RC.Customer.API.Application.Events
{
    public class CustomerAddedEvent : MediatREvent
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string NationalId { get; private set; }

        public CustomerAddedEvent(long id, string name, string email, string nationalId)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            NationalId = nationalId;
        }
    }
}
