using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IAuthService
    {
        Task LogOut(string identifier);
        Task<CookiesEntity> Login(LoggedUserDto user);
        Task Register(UserDto user);
        Task ModifyPassword(string id, string newPassword);
        Task SendForgotPasswordEmail(string email);
        Task ConfirmEmail(string id);
    }
}
