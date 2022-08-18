using FluentValidation.Results;

namespace RC.Core.Messages.IntegrationEvents
{
    public class ResponseIntegrationMessage : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseIntegrationMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
