using FluentValidation.Results;
using MediatR;

namespace RC.Core.Messages
{
    public abstract class MediatRCommand : Command, IRequest<ValidationResult>
    {
    }
}
