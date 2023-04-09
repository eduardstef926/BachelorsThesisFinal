using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IUserSymptomMappingRepository
    {
        Task AddUserSymptomMappingAsync(UserSymptomMapping userSymptom);
        Task<List<SymptomEntity>> GetAllSymptomsAsync();
    }
}
