﻿using FluentValidation.Results;
using MediatR;
using RC.Core.Messages;

namespace RC.Core.Mediator
{
    public class MediatRHandler
    {
        private readonly IMediator _mediator;

        public MediatRHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEventAsync<T>(T eventToPublish) where T : Event
        {
            await _mediator.Publish(eventToPublish);
        }

        public async Task<ValidationResult> SendCommandAsync<T>(T commandToSend) where T : MediatRCommand
        {
            return await _mediator.Send(commandToSend);
        }
    }
}
