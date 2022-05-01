using EasyNetQ;

namespace RC.MessageBus.EasyNetQ
{
    public interface IEasyNetQBus : IMessageBus
    {
        IAdvancedBus AdvancedBus { get; }
    }
}
