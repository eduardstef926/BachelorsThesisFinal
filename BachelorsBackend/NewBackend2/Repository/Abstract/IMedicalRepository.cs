using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IMedicalRepository
    {
        Task AddDiagnosticAsync(DiagnosticEntity diagnostic);
        Task AddSymptomAsync(SymptomEntity symptom);
        Task AddDiseaseAsync(DiseaseEntity disease);
        Task<int> GetSymptomsNumberAsync();
        Task<int> GetSymptomsNumberAsync(string? symptom);
        Task<List<string>> FilterSymptomsAsync(string symptom, int pageStart);
        Task<List<string>> GetAllSymptomsInRangeAsync(int pageStart);
        Task<DiagnosticEntity> GetLastDiagnosticByUserIdAsync(int id);
    }
}
