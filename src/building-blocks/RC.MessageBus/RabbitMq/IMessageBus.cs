using EasyNetQ.Internals;
using RC.Core.Messages;
using RC.Core.Messages.IntegrationEvents;

namespace RC.MessageBus.RabbitMq
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }

        // Publishes (PubSub)
        void Publish<T>(T message) where T : Event;
        Task PublishAsync<T>(T message) where T : Event;

        // Subscribes (PubSub)
        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;
        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

        // Requests (RPC)
        TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : Command;
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : Command;
        Task<TResponse> RequestIntegrationAsync<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent where TResponse : ResponseIntegrationMessage;

        // Responses (RPC)
        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder) where TRequest : Command;
        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder) where TRequest : Command;
        AwaitableDisposable<IDisposable> RespondIntegrationAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder) where TRequest : IntegrationEvent where TResponse : ResponseIntegrationMessage;
    }
}
