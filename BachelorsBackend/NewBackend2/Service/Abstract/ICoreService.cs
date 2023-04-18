using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface ICoreService
    {
        Task AddUserSymptomsAsync(string userEmail, string symptoms);
        Task UpdateSymptomsAsync();
        Task<DiagnosticDto> GetLastDiagnosticByUserEmailAsync(string email);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
