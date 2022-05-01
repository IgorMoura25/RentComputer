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
            using var scope = _serviceProvider.CreateScope();
            var productCommandHandler = scope.ServiceProvider.GetRequiredService<IProductCommandHandler>();

            _bus.RespondAsync<AddProductCommand, ValidationResult>(async request => await productCommandHandler.AddProduct(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponders();
        }
    }
}
