using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Cookie
{
    public class CookieHelpers
    {

        public static readonly string SeparatorToken = "; ";
        public static readonly string EqualsToken = "=";
        public static readonly int ExpiresDateLength = 29;
        public static readonly string ExpiresDateFormat = "r";
        public static readonly Regex TokenRegex = new(@"^[a-zA-Z0-9\-_]{86}$"); // 64-byte Base64URL

        public static readonly string AllowedNamePattern = @"^[a-zA-Z0-9_-]+$";
        public static readonly string AllowedDomainPattern = @"^(\*\.)?[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$";
        public static readonly int MaxValueLength = 4096;


    }
}
