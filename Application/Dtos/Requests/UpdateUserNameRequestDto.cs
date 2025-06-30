

namespace Application.Dtos.Requests
{
    public class UpdateUserNameRequestDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string DeviceFingerPrint { get; set; }   

        public UpdateUserNameRequestDto(Guid id, string userName, string email,  string ipAddress, string deviceFingerPrint)
        {
            Id = id;
            UserName = userName;
            Email = email;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;
        }   

    }
}
