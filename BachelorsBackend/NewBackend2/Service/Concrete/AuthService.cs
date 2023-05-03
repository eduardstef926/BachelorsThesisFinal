using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Helpers;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService emailService;
        private readonly ICookieRepository cookieRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;    

        public AuthService(ICookieRepository cookieRepository, IEmailService emailService, IUserRepository userRepository, IMapper mapper)
        {
            this.cookieRepository = cookieRepository;
            this.emailService = emailService;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<int> Login(LoggedUserDto user)
        {
            var hashedPassword = PasswordService.HashPassword(user.Password);

            if (await userRepository.CheckUserInformationAsync(user.Email, hashedPassword))
            {
                var userId = await userRepository.GetUserIdByEmailAsync(user.Email);

                var cookieEntity = new CookiesEntity
                {
                    UserId = userId,
                    DateTime = DateTime.Now.AddMonths(1),
                };

                await cookieRepository.AddCookieAsync(cookieEntity);

                return cookieEntity.CookieId;
            }

            return -1;
        }

        public async Task Register(UserDto user)
        {
            var userEntity = mapper.Map<UserDto, UserEntity>(user);
            userEntity.Password = PasswordService.HashPassword(user.Password);
            userEntity.isEmailConfirmed = false;

            await userRepository.AddUserAsync(userEntity);
            await emailService.SendWelcomeEmailAsync(user.FirstName, user.LastName);
        }

        public async Task ModifyPassword(int id, string newPassword)
        {
            var hashedPassword = PasswordService.HashPassword(newPassword);

            await userRepository.UpdateUserPasswordAsync(id, hashedPassword);
        }

        public async Task<bool> ConfirmEmail(string email, int confirmationCode)
        {
            var code = await userRepository.GetConfirmationCodeByEmailAsync(email);
            
            if (code == confirmationCode)
            {
                await userRepository.ConfirmEmailAsync(email);
                
                return true;
            }

            return false;
        }

        public async Task LogOut(int id)
        {
            await cookieRepository.DeleteCookieAsync(id);
        }

        public async Task<bool> CheckLoginCookie(int id)
        {
            return await cookieRepository.CheckCookieAsync(id);
        }
    }
}
