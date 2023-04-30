using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Abstract
{
    public interface ICookieRepository
    {
        Task AddCookie(CookiesEntity cookie);
        Task DeleteCookie(string identifier); 
    }
}
