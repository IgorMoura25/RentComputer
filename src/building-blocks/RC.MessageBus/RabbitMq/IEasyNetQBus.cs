using EasyNetQ;

namespace RC.MessageBus.RabbitMq
{
    public interface IEasyNetQBus : IMessageBus
    {
        IAdvancedBus AdvancedBus { get; }
    }
}
