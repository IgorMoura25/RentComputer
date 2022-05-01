using EasyNetQ.Internals;
using RC.Core.Messages;

namespace RC.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        void Publish<T>(T message) where T : Event;

        Task PublishAsync<T>(T message) where T : Event;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : Command;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : Command;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder) where TRequest : Command;

        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder) where TRequest : Command;
    }
}
