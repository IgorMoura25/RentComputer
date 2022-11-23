using RC.Core.Messages.IntegrationEvents;

namespace RC.MessageBus
{
    public interface IKafkaMessageBus : IDisposable
    {
        Task ProduceAsync<T>(string topic, T message) where T : IntegrationEvent;

        // executeAfterConsumed: Assim que ele consumir a mensagem, qual função irá executar?
        Task ConsumeAsync<T>(string topic, Func<T?, Task> executeAfterConsumed, CancellationToken cancellation) where T : IntegrationEvent;
    }
}
