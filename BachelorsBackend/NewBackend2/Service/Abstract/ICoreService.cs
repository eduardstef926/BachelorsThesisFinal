using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface ICoreService
    {
        Task AddUserSymptomsAsync(string userEmail, List<string> symptomNameList);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
