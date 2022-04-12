using FluentValidation.Results;

namespace RC.Core.Messages
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public virtual bool IsValid()
        {
            return false;
        }
    }
}
