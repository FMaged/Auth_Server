using Domain.Enums;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User.UserPassword
{
    public sealed class HashedPassword : ValueObject
    {

        public string Hash { get; }
        private static readonly string Pattern = @"^\$argon2id\$v=(\d+)\$m=(\d+),t=(\d+),p=(\d+)\$([A-Za-z0-9+/]+={0,2})\$([A-Za-z0-9+/]+={0,2})$";

        private HashedPassword(string hashedPassword)
        {
            Hash = hashedPassword;

        }

        public static Result<HashedPassword> Create(string password)
        {
            if (string.IsNullOrEmpty(password))
                return Result<HashedPassword>.Failure("Password hash cannot be empty");

            if (!Regex.IsMatch(password, Pattern))
                return Result<HashedPassword>.Failure("Invalid hash format for algorithm");








            return Result<HashedPassword>.Success(new HashedPassword(password));
        }







        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}
