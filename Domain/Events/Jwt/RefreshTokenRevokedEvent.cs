using Domain.Entities.Jwt;
using Domain.ValueObjects.User;

namespace Domain.Events.Jwt
{
    public class RefreshTokenRevokedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public DeviceFingerprint DeviceFingerprint { get; }
        public IpAddress IpAddress { get; }
        public Guid RefreshTokenId { get; }
        public Guid JwtId { get; }

        public RefreshTokenRevokedEvent(Guid userId, DeviceFingerprint deviceFingerprint, IpAddress ipAddress, Guid refreshTokenId, Guid jwtId)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            RefreshTokenId = refreshTokenId;
            JwtId = jwtId;
        }
        public RefreshTokenRevokedEvent(RefreshToken refreshToken)
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
