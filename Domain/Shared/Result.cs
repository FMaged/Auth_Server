
namespace Domain.Shared
{    
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        private Result( string error, bool isSuccess = false)
            => (IsSuccess, Error) = (isSuccess, error);

        public static Result Success() => new(string.Empty, true);
        public static Result Failure(string error) => new(error);
    }
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        private Result(T value, bool isSuccess, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result<T> Success(T value) => new(value, true, string.Empty);
        public static Result<T> Failure(string error) => new(default!, false, error);
    }

}
