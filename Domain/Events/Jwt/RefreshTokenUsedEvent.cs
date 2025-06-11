using Domain.Entities.Jwt;
using Domain.ValueObjects.User;

namespace Domain.Events.Jwt
{
    public class RefreshTokenUsedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }
        public Guid RefreshTokenId { get; } // Unique identifier for the refresh token
        public Guid JwtId { get; }
        public RefreshTokenUsedEvent(Guid userId, DeviceFingerprint deviceFingerprint,
                                     IpAddress ipAddress, Guid refreshTokenId)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            RefreshTokenId = refreshTokenId;
        }
        public RefreshTokenUsedEvent(RefreshToken refreshToken)
        {

            OccurredOn = DateTime.UtcNow;
            UserId = refreshToken.UserId;
            DeviceFingerprint = refreshToken.DeviceFingerprint;
            IpAddress = refreshToken.IpAddress;
            RefreshTokenId = refreshToken.Id;
            JwtId = refreshToken.JwtId;
        }

    }
}
