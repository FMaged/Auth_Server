namespace Application.Dtos.Requests
{
    public class RegisterUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string[] PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public string DeviceFingerPrint { get; set; }

        public RegisterUserRequestDto(string userName, string password, string email,
            string[] phoneNumber, string ipAddress, string deviceFingerPrint)
        {
            UserName = userName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;
        }

    }
}
