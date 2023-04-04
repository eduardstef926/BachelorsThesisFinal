using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IEmailRepository
    {
        Task AddEmailAsync(EmailEntity email);
    }
}
