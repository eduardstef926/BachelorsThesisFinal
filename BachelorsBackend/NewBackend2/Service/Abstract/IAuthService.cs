using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IAuthService
    {
        Task<bool> Login(string email, string password);
        Task Register(UserDto user);
        Task ModifyPassword(string id, string newPassword);
        Task SendForgotPasswordEmail(string email);
        Task ConfirmEmail(string id);
    }
}
