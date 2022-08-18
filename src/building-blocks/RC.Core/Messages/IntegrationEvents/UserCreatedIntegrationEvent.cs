namespace RC.Core.Messages.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? NationalId { get; private set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserCreatedIntegrationEvent(Guid id, string? name, string? email, string? nationalId, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            NationalId = nationalId;
            IsActive = isActive;
            CreatedAt = createdAt;
        }
    }
}
