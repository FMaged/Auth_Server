using Domain.Events.TokenEvents;
using Domain.Shared;
using Domain.ValueObjects.User;

namespace Domain.Entities.Jwt
{
    public class RefreshToken : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DeviceFingerprint DeviceFingerprint { get; private set; }
        public IpAddress IpAddress { get; private set; }
        public Guid JwtId { get; set; }
        public bool IsRevoked { get; private set; }
        public bool IsUsed { get; private set; }

        private RefreshToken(
           Guid id,
           Guid userId,
           string token,
           DateTime expiresAt,
           DeviceFingerprint deviceFingerprint,
           IpAddress ipAddress,
           Guid jwtId,
           bool isRevoked,
           bool isUsed) : base(id)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            IsRevoked = isRevoked;
            JwtId = jwtId;
            IsUsed = isUsed;
        }
        public static Result<RefreshToken> Create(
            Guid userId,
            string tokenHash,
            DateTime expiresAt,
            DeviceFingerprint deviceFingerprint,
            IpAddress ipAddress,
            Guid jwtId,
            bool isRevoked,
            bool isUsed)
        {
            if (expiresAt <= DateTime.UtcNow)
                return Result<RefreshToken>.Failure("Refresh token expiration date must be in the future.");
            return Result<RefreshToken>.Success(new RefreshToken(Guid.NewGuid(), userId, tokenHash, expiresAt, deviceFingerprint, ipAddress, jwtId, isRevoked, isUsed));
        }

        public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;
        public Result MarkAsUsed()
        {
            if (IsExpired())
                return Result.Failure("Refresh token is already expired.");
            if (IsUsed)
                return Result.Failure("Refresh token is already used.");
            if(IsRevoked)
                return Result.Failure("Refresh token is revoked.");

            IsUsed = true;
            return Result.Success();
        }
        public Result MarkAsRevoked()
        {
            if(IsExpired())
                return Result.Failure("Refresh token is already expired.");
            if (IsUsed)
                return Result.Failure("Refresh token is already used.");
            if (IsRevoked)
                return Result.Failure("Refresh token is revoked.");

            IsRevoked = true;
            return Result.Success();
        }

    }
}
