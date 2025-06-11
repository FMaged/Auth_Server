using Domain.Shared;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;


namespace Domain.ValueObjects.OpenID
{
    public sealed class id_token : ValueObject
    {
        public JwtAccessToken JwtAccessToken { get; }
        public Email Email { get; }
        public DeviceFingerprint DeviceFingerprint { get; }

        private id_token(JwtAccessToken jwtAccessToken, Email email, DeviceFingerprint deviceFingerprint)
        {
            JwtAccessToken = jwtAccessToken;
            Email = email;
            DeviceFingerprint = deviceFingerprint;
        }
        public static Result<id_token> Create(JwtAccessToken jwtAccessToken, Email email, DeviceFingerprint deviceFingerprint)
        {
            if (jwtAccessToken.IsExpired())
                return Result<id_token>.Failure("JWT access token is expired.");


            return Result<id_token>.Success(new id_token(jwtAccessToken, email, deviceFingerprint));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return JwtAccessToken;
            yield return Email;
            yield return DeviceFingerprint;
        }
    }
}
