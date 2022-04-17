using MediatR;

namespace RC.Customer.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerAddedEvent>
    {
        public async Task Handle(CustomerAddedEvent notification, CancellationToken cancellationToken)
        {
            // Fazer algo...
        }
    }
}
