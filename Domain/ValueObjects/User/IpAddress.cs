using Domain.Enums;
using Domain.Shared;
using System.Net;

namespace Domain.ValueObjects.User
{
    public class IpAddress : ValueObject
    {
        public string Value { get; }
        public IpVersion Version { get; }

        private IpAddress(string value, IpVersion version)
        {
            Value = value;
            Version = version;
        }
        public static Result<IpAddress> Create(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return Result<IpAddress>.Failure("IP address cannot be empty.");
            var normalized = ipAddress.Trim().ToLowerInvariant();
            if (IPAddress.TryParse(normalized, out var parsedIp))
            {
                var version = parsedIp.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
                    ? IpVersion.IPv6
                    : IpVersion.IPv4;

                return Result<IpAddress>.Success(new IpAddress(normalized, version));


            }
            return Result<IpAddress>.Failure("Invalid IP address format.");



        }



        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
