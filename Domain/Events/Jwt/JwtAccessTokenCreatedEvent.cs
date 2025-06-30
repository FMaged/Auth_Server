using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;
using System.Security.Claims;

namespace Domain.Events.Jwt
{
    public class JwtAccessTokenCreatedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }
        public Guid JWTId { get; }      // Unique identifier for the JWT
        public string Issuer { get; }
        public string Audience { get; }
        public DateTime Expiry { get; }

        public List<Claim> Claims { get; }


        public JwtAccessTokenCreatedEvent(Guid userId, DeviceFingerprint deviceFingerprint,
                    IpAddress ipAddress, Guid jWTId, string issuer, string audience, DateTime expiry,
                    List<Claim> claims)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            JWTId = jWTId;
            Issuer = issuer;
            Audience = audience;
            Expiry = expiry;
            Claims = claims;
        }
        public JwtAccessTokenCreatedEvent(JwtAccessToken token, DeviceFingerprint deviceFingerprint,
                    IpAddress ipAddress)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = token.Subject;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            JWTId = token.JWTId;
            Issuer = token.Issuer;
            Audience = token.Audience;
            Expiry = token.Expiry;
            Claims = token.Claims;
        }
    }
}
