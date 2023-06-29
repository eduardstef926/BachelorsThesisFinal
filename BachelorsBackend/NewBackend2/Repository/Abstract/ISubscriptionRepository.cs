using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface ISubscriptionRepository
    {
        Task AddUserSubscriptionAsync(SubscriptionEntity subscription);
        Task DeleteUserSubscriptionAsync(int userId);
        Task<SubscriptionEntity> GetUserSubscriptionAsync(int userId);
        Task<bool> CheckUserSubscriptionAsync(int id);
    }
}
