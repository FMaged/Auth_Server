using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User
{
    //DSGVO  ????
    public sealed class DeviceFingerprint : ValueObject
    {
        public string Value { get; }
        private const string AllowedPattern = @"^\$argon2id\$v=(\d+)\$m=(\d+),t=(\d+),p=(\d+)\$([A-Za-z0-9+/]+={0,2})$";
        private DeviceFingerprint(string value) => Value = value;

        public static Result<DeviceFingerprint> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<DeviceFingerprint>.Failure("Device fingerprint cannot be empty.");

            if (!Regex.IsMatch(value, AllowedPattern))
                return Result<DeviceFingerprint>.Failure("Invalid fingerprint format (must be hexadecimal)");






            return Result<DeviceFingerprint>.Success(new DeviceFingerprint(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
