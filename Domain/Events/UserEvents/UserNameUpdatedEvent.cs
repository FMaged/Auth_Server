using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public sealed record UserNameUpdatedEvent:IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public UserName OldUserName { get; }
        public UserName NewUserName { get; }
        public Email Email { get; }
        public IpAddress IpAddress { get; }

        public DeviceFingerprint DeviceFingerprint { get; }

        public UserNameUpdatedEvent(Guid userId, UserName oldUserName, UserName newUserName, Email email,IpAddress ipAddress, DeviceFingerprint deviceFingerprint)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            OldUserName = oldUserName;
            NewUserName = newUserName;
            Email = email;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
        }
    }
}
