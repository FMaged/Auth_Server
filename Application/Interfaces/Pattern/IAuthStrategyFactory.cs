
namespace Application.Interfaces.Pattern
{
    public interface IAuthStrategyFactory
    {

        /// <summary>
        /// Creates an authentication strategy based on the provided type.
        /// </summary>
        /// <param name="type">The type of authentication strategy to create.</param>
        /// <returns>An instance of the specified authentication strategy.</returns>
        IAuthStrategy CreateAuthStrategy(string type);
    }
}
