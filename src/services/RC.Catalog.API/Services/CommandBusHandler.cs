using FluentValidation.Results;
using RC.Catalog.API.Application.Commands;
using RC.MessageBus.EasyNetQ;

namespace RC.Catalog.API.Services
{
    public class CommandBusHandler : BackgroundService
    {
        private readonly IEasyNetQBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CommandBusHandler(IEasyNetQBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponders();
            return Task.CompletedTask;
        }

        private void SetResponders()
        {
            _bus.RespondAsync<AddProductCommand, ValidationResult>(async request => await AddProduct(request));
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponders();
        }

        private async Task<ValidationResult> AddProduct(AddProductCommand request)
        {
            ValidationResult validation;

            using (var scope = _serviceProvider.CreateScope())
            {
                var commandHandler = scope.ServiceProvider.GetRequiredService<IProductCommandHandler>();
                validation = await commandHandler.AddProduct(request);
            }

            return validation;
        }
    }
}
