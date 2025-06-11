using Domain.Enums;
using Domain.Shared;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.UserPassword;
using Domain.ValueObjects.User.UserPassword.Helpers;
using infrastructure.Services;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            Result<Email> email = Email.Create("zebo@wmsk@.com");





            var Result = IpAddress.Create("127.0.0.1");
            if (!Result.IsSuccess)
                Console.WriteLine($"Error: {Result.Error}");
            IpAddress ip = Result.Value;
            var option = new HashingOptions { Algorithm = HashingAlgorithm.Argon2id };
            HashService hashService = new HashService(option);
            string HashedIp=hashService.Hash(ip.Value);
            Console.WriteLine($"Hashed Ip: {HashedIp}");



            var PasswordResult = Password.Create("MKShajshs,2n", new PasswordOptions());
            if (!PasswordResult.IsSuccess)
                Console.WriteLine($"Error: {PasswordResult.Error}");
            Password password = PasswordResult.Value;
            var saltBC = BCrypt.Net.BCrypt.GenerateSalt();
            string Salt = "5+bt5WjR4UCd+JaTTt30eA==";
            option.Algorithm = HashingAlgorithm.Bcrypt;
            HashedPassword hashedPassword = hashService.HashPassword(password, saltBC);
            if (hashService.Verify(password.Value, hashedPassword.Hash))
            {
                Console.WriteLine($"Verifyed: {hashedPassword.Hash} ");
            }









        }
    }
}
