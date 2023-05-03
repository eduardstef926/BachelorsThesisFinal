using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserEntity user);
        Task UpdateConfirmationCode(string email, int code);
        Task UpdateUserPasswordAsync(int id, string newPassword);
        Task UpdateUserDataAsync(UserEntity user);
        Task ConfirmEmailAsync(string email);
        Task<int> GetUserIdByEmailAsync(string email);
        Task<int> GetConfirmationCodeByEmailAsync(string email);
        Task<bool> CheckUserInformationAsync(string email, string password);
        Task<string> GetUserPasswordByIdAsync(int id);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<UserEntity> GetUserByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}
