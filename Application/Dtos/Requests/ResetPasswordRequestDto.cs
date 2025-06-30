namespace Application.Dtos.Requests
{
    public class ResetPasswordRequestDto
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string DeviceFingerprint { get; set; }

        public ResetPasswordRequestDto(Guid id, string oldPassword, string newPassword, string email, string ipAddress, string deviceFingerprint)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            Email = email;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
        }




    }
}
