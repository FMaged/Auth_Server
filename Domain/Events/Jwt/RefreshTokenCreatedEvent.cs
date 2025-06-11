using Domain.Entities.Jwt;
using Domain.ValueObjects.User;

namespace Domain.Events.Jwt
{
    public class RefreshTokenCreatedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public DateTime ExpiresAt { get; }
        public Guid RefreshTokenId { get; } // Unique identifier for the refresh token
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }
        public Guid JwtId { get; } // Unique identifier for the JWT

        public RefreshTokenCreatedEvent(Guid userId, DateTime expiresAt, Guid refreshTokenId, DeviceFingerprint deviceFingerprint,
            IpAddress ipAddress, Guid jwtId)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            ExpiresAt = expiresAt;
            RefreshTokenId = refreshTokenId;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            JwtId = jwtId;
        }
        public RefreshTokenCreatedEvent(RefreshToken token)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = token.UserId;
            ExpiresAt = token.ExpiresAt;
            RefreshTokenId = token.Id;
            DeviceFingerprint = token.DeviceFingerprint;
            IpAddress = token.IpAddress;
            JwtId = token.JwtId;
        }

    }

}



