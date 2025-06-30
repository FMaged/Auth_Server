


using Domain.Shared;
using Domain.Validators;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string CountryCode { get; }
        public string Number { get; }
        public bool IsVerified { get; }
        private PhoneNumber(string countryCode, string number, bool isVerified)
        {
            CountryCode = countryCode;
            Number = number;
            IsVerified = isVerified;
        }

        public static Result<PhoneNumber> Create(string countryCode, string number, bool isVerified=false)
        {
            PhoneNumberValidator validator = new PhoneNumberValidator();
            if (!validator.Validate(countryCode, number, out List<string> errors))
                return Result<PhoneNumber>.Failure(string.Join(",\n", errors));



            return Result<PhoneNumber>.Success(new PhoneNumber(countryCode.Trim(), number.Trim(),isVerified));
        }

        
        public bool IsNumberVerified()=>IsVerified;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
