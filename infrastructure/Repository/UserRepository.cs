

using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;

namespace infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUserAsListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmailAsync(Email email)
        {
            


            UserTimestamps timestamps = new UserTimestamps(DateTime.UtcNow, null, null, null, null, null);
            UserSecurityData securityData = new UserSecurityData(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), false, 0);
            string[] numbers = { "0094", "16793092171" };
            User user= User.Create("zebo@wmsk.com", "$argon2id$v=19$m=65536,t=4,p=4$VRpXpX4emB+oscZskQAkMA==$OzRXbcktsHfOtDfg6ofZXIP0PiVYLo44is1goxsR4RI=", "SHAkazolo", true, timestamps, "127.0.0.1", "$argon2id$v=19$m=65536,t=4,p=4$ea728VCWHlABEiWPkpjE/iwteqV/7P9cJBzW5mKGMU8=", numbers, securityData).Value;

            return await Task.FromResult(user);
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUserNameAsync(UserName userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid id, User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
