


namespace Application.Dtos.Responses
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }   
        public string DeviceFingerPrint { get; set; }
        



        public UserResponseDto(Guid id, string email, string password, string userName, string phoneNumber, string ipAddress, string deviceFingerPrint)
        {
            Id = id;
            Email = email;
            Password = password;
            UserName = userName;
            PhoneNumber = phoneNumber;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;
        }


    }
}
