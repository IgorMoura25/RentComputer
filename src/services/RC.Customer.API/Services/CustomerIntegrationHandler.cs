using FluentValidation.Results;
using RC.Core.Messages.IntegrationEvents;
using RC.Customer.API.Application.Commands;
using RC.MessageBus.EasyNetQ;
using RC.MessageBus.Mediator;

namespace RC.Customer.API.Services
{
    public class CustomerIntegrationHandler : BackgroundService
    {
        private readonly IEasyNetQBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CustomerIntegrationHandler(
            IEasyNetQBus bus,
            IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        // Registrando o Respond
        // do Request-Response RPC
        private void SetResponder()
        {
            // Podendo registrar N Responders...
            _bus.RespondIntegrationAsync<UserCreatedIntegrationEvent, ResponseIntegrationMessage>(async request => await ExecuteUserCreatedIntegrationEventRequest(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }


        // Se o Message Bus se conectar
        // com o Broker, dá um refresh no respond
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        // O método que será chamado pelo Request
        // e que terá que responder
        private async Task<ResponseIntegrationMessage> ExecuteUserCreatedIntegrationEventRequest(UserCreatedIntegrationEvent message)
        {
            var command = new AddCustomerCommand(0, message.Name, message.Email, message.NationalId);
            ValidationResult success;

            // Criação de Scoped em um life cycle Singleton(this)
            // para usar classe Scoped para enviar o comando
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatRHandler>();
                success = await mediator.SendCommandAsync(command);
            }

            return new ResponseIntegrationMessage(success);
        }
    }
}
