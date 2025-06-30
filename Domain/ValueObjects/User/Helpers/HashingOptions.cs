namespace Domain.ValueObjects.User.Helpers
{
    public class HashingOptions
    {
        public const string Section = "HashingOptions";
        public int SaltSize { get; set; }
        public int MemorySize { get; set; }   //For Argon2, this is the amount of memory to use in KB.
        public int Parallelism { get; set; }   //Number of parallel threads to use for hashing.
        public int Iterations { get; set; }      //this is the number of iterations to perform.
        public int HashSize { get; set; }        //Length of the derived key in bytes.


        public HashingOptions() { }

    }
}
