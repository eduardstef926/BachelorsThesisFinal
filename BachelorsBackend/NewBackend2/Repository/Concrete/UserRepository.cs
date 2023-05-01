using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public UserRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddUserAsync(UserEntity user)
        {
            database.users.Add(user);
            await database.SaveChangesAsync();
        }

        public Task<SubscriptionEntity> GetUserSubscriptionAsync(string email)
        {
            return database.subscriptions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.Email == email);
        }

        public async Task ConfirmEmailAsync(int id)
        {
            var userToUpdate = database.users.FirstOrDefault(u => u.UserId == id);
            if (userToUpdate != null)
            {
                userToUpdate.isEmailConfirmed = true;
                database.SaveChanges();
            }
        }

        public Task<List<UserEntity>> GetAllUsersAsync()
        {
            return database.users
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return database.users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<UserEntity> GetUserByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            return database.users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public Task<int> GetUserIdByEmailAsync(string email)
        {
            return database.users
                .Where(x => x.Email == email) 
                .Select(x => x.UserId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserPasswordAsync(int id, string newPassword)
        {
            var userToUpdate = database.users.FirstOrDefault(u => u.UserId == id);
            
            if (userToUpdate != null)
            {
                userToUpdate.Password = newPassword;
                database.SaveChanges();
            }
        }

        public async Task UpdateUserDataAsync(UserEntity user)
        {
            database.users.Update(user);
            await database.SaveChangesAsync();
        }

        public Task<string> GetUserPasswordByIdAsync(int id)
        {
            return database.users.AsNoTracking()
                .Where(x => x.UserId == id)
                .Select(x => x.Password)
                .FirstOrDefaultAsync();
        }
    }
}
