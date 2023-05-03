using Microsoft.EntityFrameworkCore;
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

        public Task<SubscriptionEntity> GetUserSubscriptionAsync(string email)
        {
            return database.subscriptions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.Email == email)!;
        }

        public async Task<bool> CheckUserSubscriptionAsync(int id)
        {
            return database.subscriptions
                .Any(x => x.UserId == id && DateTime.Now.CompareTo(x.StartDate) >= 0 && DateTime.Now.CompareTo(x.EndDate) <= 0);
        }

        public async Task DeleteUserSubscriptionAsync(int userId)
        {
            var subscriptionsToRemove = await database.subscriptions.FirstOrDefaultAsync(s => s.UserId == userId);
            if (subscriptionsToRemove != null)
            {
                database.subscriptions.Remove(subscriptionsToRemove);
                await database.SaveChangesAsync();
            }
        }
    }
}
