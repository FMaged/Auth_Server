

using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public class UserEmailConfirmedEvent:IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public Email Email { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }

        public UserEmailConfirmedEvent(Guid userId, Email email, 
                        DeviceFingerprint deviceFingerprint, IpAddress ipAddress)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            Email = email;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
        }
    }
}
