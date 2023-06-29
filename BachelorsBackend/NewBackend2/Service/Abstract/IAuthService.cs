using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IAuthService
    {
        Task LogOut(int id);
        Task<int> Login(LoggedUserDto user);
        Task Register(UserDto user);
        Task<bool> ModifyPassword(int id, string newPassword);
        Task<bool> CheckLoginCookie(int id);
        Task<bool> ConfirmEmail(string email, int confirmationCode);
    }
}
