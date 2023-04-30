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

        public async Task<CookiesEntity> Login(LoggedUserDto user)
        {
            var users = await userRepository.GetAllUsersAsync();
            var hashedPassword = PasswordService.HashPassword(user.Password);

            var isUserPresent = users.Any(x => x.Email == user.Email && x.Password == hashedPassword);
            if (isUserPresent)
            {
                var cookieEntity = new CookiesEntity
                {
                    Name = user.Name,
                    Identifier = user.Identifier,
                    DateTime = DateTime.Now.AddMonths(1),
                };

                await cookieRepository.AddCookie(cookieEntity);
                return cookieEntity;
            }
            return null;
        }

        public async Task Register(UserDto user)
        {
            var userEntity = mapper.Map<UserDto, UserEntity>(user);
            userEntity.Password = PasswordService.HashPassword(user.Password);
            userEntity.isEmailConfirmed = false;

            await userRepository.AddUserAsync(userEntity);
            await emailService.SendWelcomeEmailAsync(user.FirstName, user.LastName);
        }

        public async Task ModifyPassword(string id, string newPassword)
        {
            var userId = Convert.ToInt32(id);
            var hashedPassword = PasswordService.HashPassword(newPassword);

            await userRepository.UpdateUserPasswordAsync(userId, hashedPassword);
        }

        public async Task SendForgotPasswordEmail(string email)
        {
            await emailService.SendForgotPasswordEmailAsync(email);
        }

        public async Task ConfirmEmail(string id)
        {
            await userRepository.ConfirmEmailAsync(Convert.ToInt32(id));
        }

        public async Task LogOut(string identifier)
        {
            await cookieRepository.DeleteCookie(identifier);
        }
    }
}
