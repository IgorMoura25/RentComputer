using MediatR;

namespace RC.Catalog.API.Application.Events
{
    public class ProductEventHandler : INotificationHandler<ProductAddedEvent>
    {
        public async Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
        {
            // Fazer algo...
        }
    }
}
