using Domain.Enums;
using Domain.Shared;
using Domain.ValueObjects.User.UserPassword.Helpers;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User.UserPassword
{
    public sealed class HashedPassword : ValueObject
    {

        public string Hash { get; }
        public string Salt { get; }
        public HashingAlgorithm Algorithm { get; }

        private HashedPassword(string hashedPassword, string salt, HashingAlgorithm algorithm)
        {
            Hash = hashedPassword;
            Salt = salt;
            Algorithm = algorithm;
        }

        public static Result<HashedPassword> Create(string password, string salt, HashingAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(password))
                return Result<HashedPassword>.Failure("Password hash cannot be empty");
            if (!HashingOptions.HashPatterns.TryGetValue(algorithm, out var pattern))
                return Result<HashedPassword>.Failure("Unsupported hashing algorithm");
            if (!Regex.IsMatch(password, pattern))
                return Result<HashedPassword>.Failure("Invalid hash format for algorithm");
            if (string.IsNullOrEmpty(salt))
                return Result<HashedPassword>.Failure("Salt cannot be empty");







            return Result<HashedPassword>.Success(new HashedPassword(password, salt, algorithm));
        }







        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
            yield return Algorithm;
        }
    }
}
