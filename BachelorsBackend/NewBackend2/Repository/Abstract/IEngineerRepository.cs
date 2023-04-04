using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IEngineerRepository
    {
        Task AddEngineerAsync(EngineerEntity doctor);
        Task<List<EngineerEntity>> GetAllEngineersAsync();
    }
}
