using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserAsListAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUserNameAsync(UserName userName);
        Task<User> GetUserByEmailAsync(Email email);
        Task<bool> UpdateAsync(Guid id, User newUser);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddAsync(User user);

    }
}
