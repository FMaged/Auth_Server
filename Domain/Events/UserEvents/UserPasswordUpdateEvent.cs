using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public sealed record UserPasswordUpdateEvent:IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public UserName UserName { get; }
        public Email Email { get; }
        public IpAddress IpAddress { get; }
        public DeviceFingerprint DeviceFingerprint { get; }

        public UserPasswordUpdateEvent(Guid userId, UserName userName, Email email, IpAddress ipAddress, DeviceFingerprint deviceFingerprint)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            UserName = userName;
            Email = email;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
        }
    }
}
