using Domain.Events.SessionsEvents;
using Domain.Shared;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.User;

namespace Domain.Entities.Cookie
{
    public class CookieAuthSession:Entity<Guid>
    {
        
        public Guid UserId { get; private set; }
        public CookieToken Cookie { get; private set; } 
        public DateTime CreatedAt { get; private set; }

        public DeviceFingerprint DeviceFingerprint { get; private set; }
        public IpAddress IpAddress { get; private set; }
        public bool IsRevoked { get; private set; }
        //public string UserAgentHash { get; private set; }  // SHA256 of user agent
        //public string GeoLocation { get; private set; }   // Country/region from IP
        


        private CookieAuthSession(Guid id, Guid userId, CookieToken cookie,
                DeviceFingerprint deviceFingerprint, IpAddress ipAddress) : base(id)
        {
            UserId = userId;
            Cookie = cookie;
            CreatedAt = DateTime.UtcNow;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            IsRevoked = false;





            // Raise creation events
            RaiseCreationEvents();
        }

        public static Result<CookieAuthSession> Create(Guid userId,CookieToken cookie,
                DeviceFingerprint deviceFingerprint, IpAddress ipAddress)
        {
            
            
            
            return Result<CookieAuthSession>.Success(new CookieAuthSession(Guid.NewGuid(),userId,cookie,
                deviceFingerprint,ipAddress));
        }








        private void RaiseCreationEvents()
        {
         AddDomainEvent(new CookieSessionCreatedEvent(this));

        }

    }
}
