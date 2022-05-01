using FluentValidation.Results;
using RC.Core.Messages;

namespace RC.MessageBus.Mediator
{
    public interface IMediatRHandler
    {
        public Task PublishEventAsync<T>(T eventToPublish) where T : MediatREvent;
        public Task<ValidationResult> SendCommandAsync<T>(T commandToSend) where T : MediatRCommand;
    }
}
