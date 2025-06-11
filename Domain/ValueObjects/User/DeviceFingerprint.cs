using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User
{
    //DSGVO  ????
    public sealed class DeviceFingerprint : ValueObject
    {
        public string Value { get; }
        private const int RequiredLength = 64;  //SHA-256 hash length
        private const string AllowedPattern = @"^[a-fA-F0-9]+$";//Hexadecimal 
        private DeviceFingerprint(string value) => Value = value;

        public static Result<DeviceFingerprint> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<DeviceFingerprint>.Failure("Device fingerprint cannot be empty.");
            if (value.Length != RequiredLength)
                return Result<DeviceFingerprint>.Failure($"Fingerprint must be {RequiredLength} characters");
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
