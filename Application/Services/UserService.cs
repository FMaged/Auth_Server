
using Application.Common.Exceptions.User;
using Application.Dtos.Requests;
using Application.Interfaces.Services.UserServices;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Shared;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;
using Domain.ValueObjects.User.UserPassword;


namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService; //the HashService Needs a HashingOptions object to be injected.???????
                                                    // maybe use IOptions<HashingOptions> to inject the options
        private readonly IEmailService _emailService; 
        public UserService(IUserRepository userRepository, IHashService hashService,IEmailService emailService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _emailService = emailService;
        }


        public async Task<User> GetUserByIdAsync(Guid id)    
        {
            var user=await _userRepository.GetUserByIdAsync(id)
                ?? throw new UserNotFoundException(); //Or use Result<User>.Failure("User not found");?????
            return user;
        }

        public async Task<User> RegisterUserAsync(RegisterUserRequestDto userDto)
        {
            Result<Email> emailResult=Email.Create(userDto.Email);
            if (!emailResult.IsSuccess)
            {
                throw new EmailFormatException(emailResult.Error);
            }
            Email email = emailResult.Value;
            //if(await _userRepository.GetUserByEmailAsync(email) != null)
            //{
            //    throw new EmailExistException("Email already registered");
            //}
            Result<Password> passwordResult = Password.Create(userDto.Password, new PasswordOptions());
            if(!passwordResult.IsSuccess)
            {
                throw new PasswordException(passwordResult.Error);
            }
            HashedPassword hashedPassword=_hashService.HashPassword(passwordResult.Value);
            UserTimestamps timestamps = new(DateTime.UtcNow, null, null, null, null, null);
            UserSecurityData userSecurity = new(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), false, 0);
            Result<User> userResult = User.Create(email.Value, hashedPassword.Hash, userDto.UserName, true, timestamps,
                                userDto.IpAddress, userDto.DeviceFingerPrint, userDto.PhoneNumber, userSecurity);
            if (!userResult.IsSuccess)
                throw new UserRegisterException(userResult.Error);
            if (await _userRepository.AddAsync(userResult.Value))
            {
                await _emailService.SendWelcomeEmailAsync(userResult.Value.Email.Value, userResult.Value.UserName.Value);
                return userResult.Value;
            }

            throw new UserRegisterException("Failed to persist user to the database.");

        }
        public async Task<bool> UpdateUserEmailAsync(UpdateUserEmailRequestDto userDto)
        {
            User? user = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            await _emailService.SendEmailChangeAlertAsync(user.Email.Value,"Support.Link");//the Support like if the User did not change the Email
            Result updateResult= user.UpdateEmail(userDto.NewEmail);
            if (updateResult.IsSuccess)
            {
                await _emailService.SendEmailVerificationAsync(userDto.NewEmail, "Verify.Link");//the Like to Verify the Email
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateUserNameAsync(UpdateUserNameRequestDto userDto)
        {
            User? user = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            Result updateResult = user.UpdateUserName(userDto.UserName);
            if (updateResult.IsSuccess)
            {
                //i can log the changes here 
                await _emailService.SendUserNameChangeAlert(user.Email.Value, userDto.UserName);
                return true;
            }
            return false;
        }

        public async Task<bool> ResetUserPasswordAsync(ResetPasswordRequestDto userDto)
        {
            User? user = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new UserNotFoundException("The User was NOT FOUND");
            }
            Result<Password> oldPasswordResult = Password.Create(userDto.OldPassword, new PasswordOptions());
            if (!oldPasswordResult.IsSuccess)
            {
                throw new PasswordException(oldPasswordResult.Error);
            }
            if (_hashService.Verify(oldPasswordResult.Value, user.PasswordHash))
            {
                Result<Password> passwordResult = Password.Create(userDto.NewPassword, new PasswordOptions());
                if (!passwordResult.IsSuccess)
                {
                    throw new PasswordException(passwordResult.Error);
                }
                HashedPassword hashedPassword = _hashService.HashPassword(passwordResult.Value);
                Result updateResult = user.UpdatePassword(hashedPassword);
                if (updateResult.IsSuccess)
                {
                    await _emailService.SendPasswordChangedAlertAsync(user.Email.Value,user.UserName.Value);
                    return true;
                }
                return false;
            }
            else
            {
                throw new PasswordException("Old password is incorrect.");
                
            }
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            User? user =await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (await _userRepository.DeleteAsync(id))
            {
                await _emailService.SendAccountDeletionAlertAsync(user.Email.Value,user.UserName.Value);
                return true;
            }
            return false;

        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto userDto)
        {
            User? user=await _userRepository.GetUserByIdAsync(userDto.UserId);
            if(user == null)
            {
                throw new UserNotFoundException();
            }
            if (!user.Email.IsVerified)
            {
                throw new EmailVerifiedException("Email is not verified.");
            }
            await _emailService.SendPasswordResetAsync(user.Email.Value, "Reset.Link");
            


        }

        public async Task ConfirmEmail(Guid userId)
        {
            User? user =await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (user.Email.IsVerified)
            {
              throw new EmailVerifiedException("Email is already verified.");
            }
            await _emailService.SendEmailVerificationAsync(user.Email.Value, "Verify.Link");



        }
    }
    
}
