namespace Domain.Interfaces.Services
{
    public interface IValidator<T>
    {
        bool Validate(T value, out List<string> errors);
    }
}
