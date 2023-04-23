using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public SubscriptionRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddUserSubscriptionAsync(SubscriptionEntity subscription)
        {
            database.Add(subscription);
            await database.SaveChangesAsync();
        }
    }
}
