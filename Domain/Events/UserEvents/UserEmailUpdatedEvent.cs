using Domain.ValueObjects;
using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public sealed record UserEmailUpdatedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get;}
        public Guid UserId { get; }
        public UserName UserName { get; }
        public Email NewEmail { get; }
        public Email OldEmail { get; }
        public IpAddress IpAddress { get; }
        public DeviceFingerprint DeviceFingerprint { get; }

        public UserEmailUpdatedEvent(Guid userId, UserName userName, 
                Email newEmail, Email oldEmail,IpAddress ipAddress, DeviceFingerprint deviceFingerprint)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            UserName = userName;
            NewEmail = newEmail;
            OldEmail = oldEmail;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
        }
    }
}
