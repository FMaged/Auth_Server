
using Domain.ValueObjects.User;

namespace Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
        Guid UserId { get; }
        DeviceFingerprint DeviceFingerprint { get; }
        IpAddress IpAddress { get; }

    }
}
