using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
