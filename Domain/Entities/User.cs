using Domain.Events.UserEvents;
using Domain.Shared;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;
using Domain.ValueObjects.User.UserPassword;


namespace Domain.Entities
{
    public class User:Entity<Guid>
    {
        private const int MaxFailedAttempts = 3;
        public HashedPassword PasswordHash { get; private set; }

        public Email Email { get; private set; }

        public UserName UserName { get; private set; }
        
        public bool IsActive { get; private set; }
        public DeviceFingerprint DeviceFingerprint { get; private set; }
        
        public IpAddress IpAddress { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        
        public UserSecurityData SecurityData { get; private set; }
        public UserTimestamps UserTimestamps { get; private set; }



        private User(Guid id, Email email, HashedPassword password, UserName userName, bool isActive, 
                    UserTimestamps timestamps, DeviceFingerprint deviceFingerprint, 
                    IpAddress ipAddress,PhoneNumber phoneNumber,
                    UserSecurityData userSecurityData) : base(id)

        {
            Email = email;
            PasswordHash = password;
            UserName = userName;
            IsActive = isActive;
            UserTimestamps = timestamps;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
            PhoneNumber = phoneNumber;
            SecurityData=userSecurityData;
            AddDomainEvent(new UserCreatedEvent(this.Id, this.UserName, this.Email,this.IpAddress, this.DeviceFingerprint));

        }

        public static Result<User> Create(string email, string password, string userName, bool isActive, 
                    UserTimestamps timestamps, string ipAddress, 
                    string devicePrint, string[] phoneNumber, UserSecurityData userSecurityData)
        {
            var emailResult = Email.Create(email);
            if (!emailResult.IsSuccess)
                return Result<User>.Failure(emailResult.Error);
            var passwordResult = HashedPassword.Create(password);//default hashing algorithm Argon2id
            if (!passwordResult.IsSuccess)
                return Result<User>.Failure(passwordResult.Error);
            var userNameResult = UserName.Create(userName);
            if (!userNameResult.IsSuccess)
                return Result<User>.Failure(userNameResult.Error);
            var deviceFingerprint = DeviceFingerprint.Create(devicePrint);
            if(!deviceFingerprint.IsSuccess)
                return Result<User>.Failure(deviceFingerprint.Error);
            var ipAddressResult = IpAddress.Create(ipAddress);
            if (!ipAddressResult.IsSuccess)
                return Result<User>.Failure(ipAddressResult.Error);

            var phoneNumberResult = PhoneNumber.Create(phoneNumber[0], phoneNumber[1]);
            if (!phoneNumberResult.IsSuccess)
                return Result<User>.Failure(phoneNumberResult.Error);
            


            var user = new User(Guid.NewGuid(), emailResult.Value, passwordResult.Value, userNameResult.Value,
                    isActive, timestamps, deviceFingerprint.Value,
                    ipAddressResult.Value, phoneNumberResult.Value, 
                    userSecurityData
            );

            return Result<User>.Success(user);
        }

        public static Result<User> CreateO(Guid id, Email email, HashedPassword password, UserName userName, bool isActive,
                    UserTimestamps timestamps, DeviceFingerprint deviceFingerprint, 
                    IpAddress ipAddress, PhoneNumber phoneNumber,
                    UserSecurityData userSecurityData)
        { 
           
            var user = new User(id,email,password,userName, isActive, timestamps, deviceFingerprint,
                        ipAddress, phoneNumber,userSecurityData);
            return Result<User>.Success(user);
        }

        public Result UpdateEmail(string newEmail)
        {
            var emailResult=Email.Create(newEmail);
            if(!emailResult.IsSuccess)
                return Result.Failure(emailResult.Error);
            if (Email == emailResult.Value)
                return Result.Failure("New email must be different");
            var OldEmail = Email;   
            Email = emailResult.Value;
            UserTimestamps userTimestamps = new(UserTimestamps.CreatedAt, UserTimestamps.LastLoginAt,
                    UserTimestamps.LastPasswordChangeAt, DateTime.UtcNow, UserTimestamps.LastUserNameChangeAt,
                    UserTimestamps.EmailConfirmedAt);
            UserTimestamps = userTimestamps;

            AddDomainEvent(new UserEmailUpdatedEvent(this.Id,this.UserName,this.Email,OldEmail,this.IpAddress,this.DeviceFingerprint));
            return Result.Success();
        }
        public Result VerifyEmail ()
        {
            Result<Email> emailResult = Email.Verify();
            if(!emailResult.IsSuccess)
                return Result.Failure(emailResult.Error);

            this.Email = emailResult.Value;
            UserTimestamps userTimestamps = new(UserTimestamps.CreatedAt, UserTimestamps.LastLoginAt,
                    UserTimestamps.LastPasswordChangeAt, UserTimestamps.LastEmailChangeAt, UserTimestamps.LastUserNameChangeAt,
                    DateTime.UtcNow);
            UserTimestamps = userTimestamps;



            AddDomainEvent(new UserEmailConfirmedEvent(Id,Email,DeviceFingerprint,IpAddress));
            return Result.Success();
                
        }
        public Result UpdatePassword(HashedPassword newPassword)
        {
            
            if (PasswordHash == newPassword)
                return Result.Failure("Password can not the same");
            PasswordHash = newPassword;
            UserTimestamps userTimestamps = new(UserTimestamps.CreatedAt, UserTimestamps.LastLoginAt,
                DateTime.UtcNow, UserTimestamps.LastEmailChangeAt, UserTimestamps.LastUserNameChangeAt,
                UserTimestamps.EmailConfirmedAt);
            UserTimestamps = userTimestamps;
            AddDomainEvent(new UserPasswordUpdateEvent(this.Id,this.UserName,this.Email,this.IpAddress,this.DeviceFingerprint));
            return Result.Success();
        }
        public Result UpdateUserName(string newUserName)
        {
            var userNameResult = UserName.Create(newUserName);
            if (!userNameResult.IsSuccess)
                return Result.Failure(userNameResult.Error);
            if (UserName == userNameResult.Value)
                return Result.Failure("New username must be different");
            var OldUserName=UserName;
            UserName = userNameResult.Value;
            UserTimestamps userTimestamps = new(UserTimestamps.CreatedAt, UserTimestamps.LastLoginAt,
                UserTimestamps.LastPasswordChangeAt, UserTimestamps.LastEmailChangeAt, DateTime.UtcNow,
                UserTimestamps.EmailConfirmedAt);
            UserTimestamps = userTimestamps;
            AddDomainEvent(new UserNameUpdatedEvent(this.Id, OldUserName, this.UserName,this.Email,this.IpAddress,this.DeviceFingerprint));
            return Result.Success();
        }
        
        public void IncrementFailedLogin()
        {
            var NewSecurityData = new UserSecurityData(this.SecurityData.SecurityStamp,this.SecurityData.ConcurrencyStamp, 
                this.SecurityData.TwoFactorEnabled, this.SecurityData.FailedLoginAttempts + 1);
            this.SecurityData = NewSecurityData;
            if (this.SecurityData.FailedLoginAttempts >= MaxFailedAttempts)
            {
                IsActive = false;
                AddDomainEvent(new UserDeactivatedEvent(Id,UserName,Email,DeviceFingerprint,IpAddress));
            }
        }

    }
}
