using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class CookieRepository : ICookieRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public CookieRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddCookieAsync(CookiesEntity cookie)
        {
            database.cookies.Add(cookie);
            await database.SaveChangesAsync();
        }

        public async Task DeleteCookieAsync(int id)
        {
            var cookie = database.cookies.FirstOrDefault(item => item.CookieId == id);
            if (cookie != null)
            {
                database.cookies.Remove(cookie);
                await database.SaveChangesAsync();
            }
        }

        public Task<CookiesEntity> GetCookieByIdAsync(int id)
        {
            return database.cookies.AsNoTracking()
               .Where(x => x.CookieId == id)
               .FirstOrDefaultAsync();
        }

        public Task<UserEntity> GetUserByCookieIdAsync(int id)
        {
            return database.cookies.AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.CookieId == id)
                .Select(x => x.User)
                .FirstOrDefaultAsync()!;
        }

        public Task<int> GetUserIdByCookieIdAsync(int id)
        {
            return database.cookies.AsNoTracking()
               .Where(x => x.CookieId == id)
               .Select(x => x.UserId)
               .FirstOrDefaultAsync();
        }
    }
}
