using FluentValidation.Results;
using RC.Core.Messages;

namespace RC.Core.Mediator
{
    public interface IMediatRHandler
    {
        public Task PublishEventAsync<T>(T eventToPublish) where T : Event;
        public Task<ValidationResult> SendCommandAsync<T>(T commandToSend) where T : MediatRCommand;
    }
}
