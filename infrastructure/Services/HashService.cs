using Domain.Interfaces.Services;
using Domain.ValueObjects;
using Domain.ValueObjects.User.UserPassword;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;
using Domain.Shared;
using Domain.ValueObjects.User.Helpers;


namespace infrastructure.Services
{
    public sealed class HashService : IHashService
    {
        private readonly HashingOptions _options;

        public HashService(HashingOptions options) //The IOptions interface is used to inject configuration settings into the service.
        {



            _options = options;

        }

        //Hash a string NO SALT
        public string Hash(string input)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(input);
            try
            {
                using var argon2 = new Argon2id(passwordBytes)
                {
                    DegreeOfParallelism = _options.Parallelism,
                    MemorySize = _options.MemorySize,
                    Iterations = _options.Iterations
                };
                byte[] hashBytes = argon2.GetBytes(_options.HashSize);
                string encodedHash = Convert.ToBase64String(hashBytes);
                                string str = $"$argon2id$v=19$m={_options.MemorySize},t={_options.Iterations},p={_options.Parallelism}${encodedHash}";

                return str;
            }
            finally
            {
                Array.Clear(passwordBytes, 0, passwordBytes.Length); // Clear sensitive data

            }
        }


        // Hash a Password OV with a Salt as string as Base64
        public HashedPassword HashPassword(Password password, string salt)
        {

            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password.Value);

            Result<HashedPassword> result = HashedPassword.Create(HashWithArgon2id(passwordBytes, saltBytes));
            if (!result.IsSuccess)
            {
                throw new Exception($"Failed to create hashed password: {result.Error}");
            }
            return result.Value;
        }


        // Hashing the password with generated salt
        public HashedPassword HashPassword(Password password)
        {
            
            byte[] saltBytes = CreateSalt();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password.Value);
            Result<HashedPassword> result = HashedPassword.Create(HashWithArgon2id(passwordBytes, saltBytes));
            if (!result.IsSuccess)
            {
                throw new Exception($"Failed to create hashed password: {result.Error}");
            }
            return result.Value;

        }


        //Verify the passwords with Argon2id
        public bool Verify(Password password, HashedPassword hash)
        {
            return VerifyCore(password.Value, hash.Hash);
        }

        public bool Verify(string input, string hashedInput)
        {
           return VerifyCore(input, hashedInput);
        }

        private byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(_options.SaltSize);
        }






        private bool VerifyCore(string plainText, string hashed)
        {
            string[] hashParts = hashed.Split('$');
            if (hashParts.Length < 6)
                throw new FormatException("Invalid hash format.");

            string paramsSegment = hashParts[3];
            var parameters = ParseParameters(paramsSegment);

            byte[] saltBytes = Convert.FromBase64String(hashParts[4]);
            string hashedPassword = HashWithArgon2id(
                plainText,
                saltBytes,
                parameters.Memory,
                parameters.Time,
                parameters.Threads
            );
            string actualHashPart = hashedPassword.Split('$').Last();
            string expectedHashPart = hashParts[5]; // Last part of stored hash


            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(actualHashPart),
                Convert.FromBase64String(expectedHashPart)
            );



            
        }
        private string HashWithArgon2id(byte[] passwordBytes, byte[] saltBytes)
        {
            try
            {

                using var argon2 = new Argon2id(passwordBytes)
                {
                    Salt = saltBytes,
                    DegreeOfParallelism = _options.Parallelism,
                    MemorySize = _options.MemorySize,
                    Iterations = _options.Iterations
                };

                byte[] hashBytes = argon2.GetBytes(_options.HashSize);
                string encodedSalt = Convert.ToBase64String(saltBytes);
                string encodedHash = Convert.ToBase64String(hashBytes);

                string str = $"$argon2id$v=19$m={_options.MemorySize},t={_options.Iterations},p={_options.Parallelism}${encodedSalt}${encodedHash}";

                return str;

            }
            finally
            {
                Array.Clear(saltBytes, 0, saltBytes.Length); // Clear sensitive data
                Array.Clear(passwordBytes, 0, passwordBytes.Length);

            }
        }






        private string HashWithArgon2id(string input, byte[] salt, int memorySize, int iterations, int parallelism)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(input);

            try
            {

                using var argon2 = new Argon2id(passwordBytes)
                {
                    Salt = salt,
                    DegreeOfParallelism = parallelism,
                    MemorySize = memorySize,
                    Iterations = iterations
                };

                byte[] hashBytes = argon2.GetBytes(_options.HashSize);
                string encodedSalt = Convert.ToBase64String(salt);
                string encodedHash = Convert.ToBase64String(hashBytes);

                return $"$argon2id$v=19$m={memorySize},t={iterations},p={parallelism}${encodedSalt}${encodedHash}";
            }
            finally
            {
                //Array.Clear(salt, 0, salt.Length); // Clear sensitive data
                Array.Clear(passwordBytes, 0, passwordBytes.Length);

            }

        }
        private (int Memory, int Time, int Threads) ParseParameters(string paramsSegment)
        {
            int memory = 0, time = 0, threads = 0;
            foreach (string pair in paramsSegment.Split(','))
            {
                string[] kv = pair.Split('=');
                if (kv.Length != 2) continue;

                switch (kv[0])
                {
                    case "m": memory = int.Parse(kv[1]); break;
                    case "t": time = int.Parse(kv[1]); break;
                    case "p": threads = int.Parse(kv[1]); break;
                }
            }
            return (memory, time, threads);
        }
    }
}
