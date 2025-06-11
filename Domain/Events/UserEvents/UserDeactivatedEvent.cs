using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public sealed record UserDeactivatedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public UserName UserName { get; }
        public Email Email { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }
        public UserDeactivatedEvent(Guid userId, UserName userName, Email email, DeviceFingerprint deviceFingerprint, IpAddress ipAddress)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            UserName = userName;
            Email = email;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
        }


    }
}
