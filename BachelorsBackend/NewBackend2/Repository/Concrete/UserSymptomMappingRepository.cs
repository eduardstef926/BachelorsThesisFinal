using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class UserSymptomMappingRepository : IUserSymptomMappingRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public UserSymptomMappingRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddUserSymptomMappingAsync(UserSymptomMapping userSymptom)
        {
            database.userSymptoms.Add(userSymptom);
            await database.SaveChangesAsync();
        }

        public Task<List<SymptomEntity>> GetAllSymptomsAsync()
        {
            return database.symptoms
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
