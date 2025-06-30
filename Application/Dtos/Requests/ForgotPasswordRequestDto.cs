namespace Application.Dtos.Requests
{
    public class ForgotPasswordRequestDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string DeviceFingerPrint { get; set; }

        public ForgotPasswordRequestDto(Guid userId, string email, string ipAddress, string deviceFingerPrint)
        {
            UserId = userId;
            Email = email;
            IpAddress = ipAddress;
            DeviceFingerPrint = deviceFingerPrint;
        }



    }
}
