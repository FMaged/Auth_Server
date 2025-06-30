using Domain.Entities.Cookie;
using Domain.ValueObjects.User;

namespace Domain.Events.SessionsEvents
{
    public class CookieSessionCreatedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public string SessionId { get; } 
        public Guid Id { get; }// Unique identifier for the session in the Database
        public Guid UserId { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }

        public CookieSessionCreatedEvent(string sessionId, Guid id,Guid userId, DeviceFingerprint deviceFingerprint,
            IpAddress ipAddress)
        {
            OccurredOn = DateTime.UtcNow;
            SessionId = sessionId;
            Id = id;
            UserId = userId;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;

        }
        public CookieSessionCreatedEvent(CookieAuthSession session)
        {

            OccurredOn = DateTime.UtcNow;
            SessionId = session.Cookie.SessionId;
            Id = session.Id;
            UserId = session.UserId;
            DeviceFingerprint = session.DeviceFingerprint;
            IpAddress = session.IpAddress;

        }



    }
}
