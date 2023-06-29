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

        public async Task AddDiagnosticAsync(DiagnosisEntity diagnostic)
        {
            database.diagnostics.Add(diagnostic);
            await database.SaveChangesAsync();
        }

        public Task<List<string>> GetAllSymptomsInRangeAsync(int pageStart)
        {
            return database.symptoms
                .AsNoTracking()
                .Skip(pageStart)
                .Take(5)
                .Select(x => x.Name)
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

        public Task<DiagnosisEntity> GetLastDiagnosticByUserIdAsync(int id)
        {
            return database.diagnostics.AsNoTracking()
                .OrderBy(x => x.DiagnosticId)
                .LastOrDefaultAsync(x => x.UserId == id)!;
        }

        public Task<List<string>> FilterSymptomsAsync(string symptom, int pageStart)
        {
            return database.symptoms.AsNoTracking()
                .Where(x => x.Name.Contains(symptom))
                .AsNoTracking()
                .Skip(pageStart)
                .Take(5)
                .Select(x => x.Name)
                .ToListAsync()!;
        }

        public Task<int> GetSymptomsNumberAsync(string? symptom)
        {
            return database.symptoms.AsNoTracking()
                .Where(x => x.Name.Contains(symptom))
                .AsNoTracking()
                .CountAsync()!;
        }

        public Task<int> GetSymptomsNumberAsync()
        {
            return database.symptoms
                .AsNoTracking()
                .CountAsync()!;
        }
    }
}
