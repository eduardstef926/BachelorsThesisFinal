using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserEntity user);
        Task UpdateUserPasswordAsync(int id, string newPassword);
        Task ConfirmEmailAsync(int id);
        Task<int> GetUserIdByEmailAsync(string email);
        Task<UserEntity> GetUserByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}
