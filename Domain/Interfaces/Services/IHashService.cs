

using Domain.ValueObjects;
using Domain.ValueObjects.User.UserPassword;

namespace Domain.Interfaces.Services
{
    public interface IHashService
    {
        // Hash password using provided salt 
        public HashedPassword HashPassword(Password password, string salt);


        // Hash password and generate a new salt 
        public HashedPassword HashPassword(Password password);

        // Hash an input (e.g. DeviceFingerprint) and output the HashedInput with Generated Salt
        // the salt is within the returned string
        public string Hash(string input);


        public bool Verify(Password password, HashedPassword hash);
        public bool Verify(string password, string hash);




    }
}
