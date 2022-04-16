using MediatR;

namespace RC.Core.Messages
{
    public abstract class MediatREvent : Event, INotification
    {
    }
}
