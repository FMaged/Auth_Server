

namespace Application.Dtos.Requests
{
    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? RefreshToken { get; set; } // used for refresh token flow, needs to move or refactor the class
        public string? CookieHeader { get; set; } // used for cookie-based authentication
        public string Password { get; set; }

        public string IpAddress { get; set; }
        public string? DeviceFingerPrint { get; set; }

        public string ClientType { get; set; } 

        public LoginRequest(string? username, string? email, string? refreshToken, string? cookieString, string password, string ipAddress, string? deviceFingerPrint, string clientType)
        {
            Username = username;
            Email = email;
            RefreshToken = refreshToken;
            CookieHeader = cookieString;
            Password = password;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;
            ClientType = clientType;
        }

    }
}
