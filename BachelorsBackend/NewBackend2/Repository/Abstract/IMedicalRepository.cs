using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IMedicalRepository
    {
        Task AddDiagnosticAsync(DiagnosticEntity diagnostic);
        Task AddSymptomAsync(SymptomEntity symptom);
        Task AddDiseaseAsync(DiseaseEntity disease);
        Task<DiagnosticEntity> GetLastDiagnosticByUserIdAsync(int id);
        Task<List<SymptomEntity>> GetAllSymptomsAsync();
    }
}
