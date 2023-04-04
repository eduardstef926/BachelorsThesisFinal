using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class SymptomRepository : ISymptomRespository
    {
        private readonly ProjectDatabaseConfiguration database;

        public SymptomRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddSymptomAsync(SymptomEntity symptom)
        {
            database.symptoms.Add(symptom);
            await database.SaveChangesAsync();
        }
    }
}
