

namespace Application.Dtos.Requests
{
    public class UpdateUserEmailRequestDto
    {
        public Guid Id { get; set; }
        public string NewEmail { get; set; }
        public string IpAddress { get; set; }
        public string DeviceFingerPrint { get; set; }
        public UpdateUserEmailRequestDto(Guid id, string newEmail,string ipAddress,string deviceFingerPrint)
        {
            Id = id;
            NewEmail = newEmail;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;

        }
    }
}
