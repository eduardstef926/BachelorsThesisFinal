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

        public async Task AddCookie(CookiesEntity cookie)
        {
            if (!database.cookies.Any(item => item.Identifier == cookie.Identifier))
            {
                database.cookies.Add(cookie);
                await database.SaveChangesAsync();
            }
        }

        public async Task DeleteCookie(string identifier)
        {
            var cookie = database.cookies.FirstOrDefault(item => item.Identifier == identifier);
            if (cookie != null)
            {
                database.cookies.Remove(cookie);
                await database.SaveChangesAsync();
            }
        }
    }
}
