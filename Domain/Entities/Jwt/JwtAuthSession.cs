using Domain.Events.Jwt;
using Domain.Shared;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;

namespace Domain.Entities.Jwt
{

    // For Jwt flow, is stored in the database but just the needed Fields   
    // using the JwtAuthSessionDto so i don't store the tokens 
    // First create the access token, then i create the refresh token
    // the refresh has to have the same userId and JWTId as the access token
    public class JwtAuthSession : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public JwtAccessToken Token { get; private set; }
        public RefreshToken RefreshToken { get; private set; }

        // DeviceFingerprint and IpAddress are already stored in the RefreshToken
        // do i need to store them here again?
        public DeviceFingerprint DeviceFingerprint { get; private set; }
        public IpAddress IpAddress { get; private set; }
        public DateTime CreatedAt { get; private set; }



        private JwtAuthSession(Guid id,Guid userId,JwtAccessToken token,RefreshToken refreshToken,DeviceFingerprint deviceFingerprint,
            IpAddress ipAddress) : base(id)
        {
            UserId = userId;
            Token = token;
            RefreshToken = refreshToken;
            DeviceFingerprint = deviceFingerprint;
            IpAddress = ipAddress;
            CreatedAt = DateTime.UtcNow;
            

            //  Add the events 
            RaiseCreationEvents();
        }

        public static Result<JwtAuthSession> Create(Guid userId, JwtAccessToken token, RefreshToken refreshToken, 
                DeviceFingerprint deviceFingerprint, IpAddress ipAddress)
        {
            if (token.Subject.ToString() != refreshToken.UserId.ToString())
                return Result<JwtAuthSession>.Failure("The access token and refresh token do not belong to the same user.");
            if (token.JWTId.ToString() != refreshToken.JwtId.ToString())
                return Result<JwtAuthSession>.Failure("The access token and refresh token do not belong to the same session.");
            if (token.IsExpired())
                return Result<JwtAuthSession>.Failure("JWT access token is expired.");
            if (refreshToken.IsExpired())
                return Result<JwtAuthSession>.Failure("Refresh token is expired.");

           
            return Result<JwtAuthSession>.Success(new JwtAuthSession(Guid.NewGuid(), userId, token, refreshToken, deviceFingerprint, ipAddress));
        
        
        
        
        
        }

        public Result<JwtAuthSession> UseRefreshToken()
        {
            var Result = RefreshToken.MarkAsUsed();
            if(!Result.IsSuccess)
                return Result<JwtAuthSession>.Failure(Result.Error);

            AddDomainEvent(new RefreshTokenUsedEvent(RefreshToken));
            return Result<JwtAuthSession>.Success(this);
        }
        public Result<JwtAuthSession> RevokeRefreshToken()
        {
            var Result = RefreshToken.MarkAsRevoked();
            if (!Result.IsSuccess)
                return Result<JwtAuthSession>.Failure(Result.Error);
            AddDomainEvent(new RefreshTokenRevokedEvent(RefreshToken));
            return Result<JwtAuthSession>.Success(this);
        }

        private void RaiseCreationEvents()
        {
            AddDomainEvent(new RefreshTokenCreatedEvent(RefreshToken));
            AddDomainEvent(new JwtAccessTokenCreatedEvent(Token,DeviceFingerprint,IpAddress));
        }
    }
}
