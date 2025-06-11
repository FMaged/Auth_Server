

using Domain.Enums;

namespace Domain.ValueObjects.User.UserPassword.Helpers
{
    public class HashingOptions
    {
        public const string Section = "Hashing";

        public HashingAlgorithm Algorithm { get; set; } = HashingAlgorithm.Bcrypt; 
        public int WorkFactor { get; set; } = 15;       // for BCrypt, Higher values increase security but also slow down performance.
        public int SaltSize { get; set; } = 16;
        public int MemorySize { get; set; } = 65536;    //For Argon2, this is the amount of memory to use in KB.
        public int Parallelism { get; set; } = 4;       //Number of parallel threads to use for hashing.
        public int Iterations { get; set; } = 4;        //this is the number of iterations to perform.
        public int HashSize { get; set; } = 32;         //Length of the derived key in bytes.
        
        public static Dictionary<HashingAlgorithm, string> HashPatterns = new()
        {
            [HashingAlgorithm.Bcrypt] = @"^\$2[aby]\$\d{2}\$[./A-Za-z0-9]{53}$",
            [HashingAlgorithm.Argon2id] = @"^\$argon2id\$v=(\d+)\$m=(\d+),t=(\d+),p=(\d+)\$([A-Za-z0-9+/]+={0,2})\$([A-Za-z0-9+/]+={0,2})$",
        };
    }
}
