


using Domain.Shared;
using Domain.Validators;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string CountryCode { get; }
        public string Number { get; }

        private PhoneNumber(string countryCode, string number)
        {
            CountryCode = countryCode;
            Number = number;
        }

        public static Result<PhoneNumber> Create(string countryCode, string number)
        {
            PhoneNumberValidator validator = new PhoneNumberValidator();
            if (!validator.Validate(countryCode, number, out List<string> errors))
                return Result<PhoneNumber>.Failure(string.Join(",\n", errors));



            return Result<PhoneNumber>.Success(new PhoneNumber(countryCode.Trim(), number.Trim()));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
