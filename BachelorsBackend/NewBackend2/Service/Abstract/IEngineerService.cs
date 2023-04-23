using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IEngineerService
    {
        Task AddEngineerAsync(EngineerDto engineer);
        Task<List<EngineerDto>> GetAllEngineersAsync();
    }
}
