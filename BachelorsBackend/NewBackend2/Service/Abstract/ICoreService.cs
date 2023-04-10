using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface ICoreService
    {
        Task AddUserSymptomsAsync(string userEmail, string symptoms);
        Task UpdateSymptomsAsync();
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
