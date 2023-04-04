using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class EngineerRepository : IEngineerRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public EngineerRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddEngineerAsync(EngineerEntity engineer)
        {
            database.engineers.Add(engineer);
            await database.SaveChangesAsync();
        }

        public Task<List<EngineerEntity>> GetAllEngineersAsync()
        {
            return database.engineers
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
