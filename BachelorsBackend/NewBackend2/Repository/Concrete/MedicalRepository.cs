using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class MedicalRepository : IMedicalRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public MedicalRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddDiagnosticAsync(DiagnosticEntity diagnostic)
        {
            database.diagnostics.Add(diagnostic);
            await database.SaveChangesAsync();
        }

        public Task<List<SymptomEntity>> GetAllSymptomsAsync()
        {
            return database.symptoms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddDiseaseAsync(DiseaseEntity disease)
        {
            if (!database.diseases.Any(item => item == disease))
            {
                database.diseases.Add(disease);
                await database.SaveChangesAsync();
            }
        }

        public async Task AddSymptomAsync(SymptomEntity symptom)
        {
            if (!database.symptoms.Any(item => item == symptom))
            {
                database.symptoms.Add(symptom);
                await database.SaveChangesAsync();
            }
        }
    }
}
