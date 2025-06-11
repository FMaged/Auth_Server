

using Domain.Interfaces.Services;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.ValueObjects.User.UserPassword.Helpers;
using Domain.ValueObjects.User.UserPassword;

using System.Security.Cryptography;
using Konscious.Security.Cryptography;


namespace infrastructure.Services
{
    public sealed class HashService : IHashService
    {
        private readonly HashingOptions _options;

        public HashService(HashingOptions options)
        {
            if(options.Algorithm == HashingAlgorithm.Bcrypt &&
              (options.WorkFactor < 4 || options.WorkFactor > 31))
                throw new ArgumentOutOfRangeException("BCrypt work factor must be 4-31");


            _options = options;

        }
             
        //Hash a string NO SALT
        public string Hash  (string input)
        {
            return _options.Algorithm switch
            {
                HashingAlgorithm.Bcrypt => BCrypt.Net.BCrypt.HashPassword(input, _options.WorkFactor),
                HashingAlgorithm.Argon2id => HashWithArgon2id(input),

                _ => throw new NotSupportedException($"Hashing algorithm {_options.Algorithm} is not supported.")
            };
        }


        // Hash a Password OV with a Salt as string as Base64
        public HashedPassword HashPassword(Password password, string salt)
        {

            string HashPassword = _options.Algorithm switch
            {
                
                HashingAlgorithm.Bcrypt => BCrypt.Net.BCrypt.HashPassword(password.Value,salt),// the Salt has to be generated with BCrypt
                HashingAlgorithm.Argon2id => HashWithArgon2id(password.Value, salt),

                _ => throw new NotSupportedException($"Hashing algorithm {_options.Algorithm} is not supported.")
            };
            var result=HashedPassword.Create(HashPassword,salt, _options.Algorithm);
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
            string HashPassword = _options.Algorithm switch
            {
                HashingAlgorithm.Bcrypt => BCrypt.Net.BCrypt.HashPassword(password.Value, _options.WorkFactor),
                HashingAlgorithm.Argon2id => HashWithArgon2id(password.Value, Convert.ToBase64String(saltBytes)),

                _ => throw new NotSupportedException($"Hashing algorithm {_options.Algorithm} is not supported.")
            };
            var salt = Convert.ToBase64String(saltBytes);
            var result = HashedPassword.Create(HashPassword, salt, _options.Algorithm);
            if (!result.IsSuccess)
            {
                throw new Exception($"Failed to create hashed password: {result.Error}");
            }

            return result.Value;
        }


        //Verify the passwords with Argon2id
        public bool Verify(Password password, HashedPassword hash)
        {
            string[] hashParts = hash.Hash.Split('$');
            if (hashParts.Length < 6)
                throw new FormatException("Invalid hash format.");

            string paramsSegment = hashParts[3];
            var parameters = ParseParameters(paramsSegment);

            byte[] saltBytes = Convert.FromBase64String(hash.Salt);
            string hashedPassword = HashWithArgon2id(
                password.Value,
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


        //Verify strings using BCrypt
        public bool Verify(string password, string hash)
        {
            if (_options.Algorithm != HashingAlgorithm.Bcrypt)
                throw new InvalidOperationException("Only BCrypt algorithm is supported for string verification");
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);

        }

        private byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(_options.SaltSize);
        }














        // Hash with no salt 
        private string HashWithArgon2id(string input)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(input);
            try
            {
                using var argon2 = new Argon2id(passwordBytes)
                {
                    DegreeOfParallelism = _options.Parallelism,
                    MemorySize = _options.MemorySize,
                    Iterations = _options.Iterations
                };
                byte[] hashBytes = argon2.GetBytes(_options.HashSize);
                return Convert.ToBase64String(hashBytes);
            }
            finally
            {
                Array.Clear(passwordBytes, 0, passwordBytes.Length); // Clear sensitive data

            }
        }
        private string HashWithArgon2id(string input, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(input);

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

                return $"$argon2id$v=19$m={_options.MemorySize},t={_options.Iterations},p={_options.Parallelism}${encodedSalt}${encodedHash}";
            }
            finally
            {
                //Array.Clear(salt, 0, salt.Length); // Clear sensitive data
                Array.Clear(passwordBytes, 0, passwordBytes.Length);

            }

        }
        private string HashWithArgon2id(string input, byte[] salt, int memorySize,int iterations,int parallelism)
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

                return $"$argon2id$v=19$m={_options.MemorySize},t={_options.Iterations},p={_options.Parallelism}${encodedSalt}${encodedHash}";
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
