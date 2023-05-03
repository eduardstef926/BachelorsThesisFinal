using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Abstract
{
    public interface ICookieRepository
    {
        Task AddCookieAsync(CookiesEntity cookie);
        Task DeleteCookieAsync(int id);
        Task<bool> CheckCookieAsync(int id);
        Task<int> GetUserIdByCookieIdAsync(int id);
        Task<UserEntity> GetUserByCookieIdAsync(int id);
    }
}
