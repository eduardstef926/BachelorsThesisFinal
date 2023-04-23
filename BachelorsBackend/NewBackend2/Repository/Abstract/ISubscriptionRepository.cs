using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface ISubscriptionRepository
    {
        Task AddUserSubscriptionAsync(SubscriptionEntity subscription);
    }
}
