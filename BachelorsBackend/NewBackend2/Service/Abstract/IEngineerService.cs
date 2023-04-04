using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IEngineerService
    {
        public Task AddEngineerAsync(EngineerDto engineer);
        public Task<List<EngineerDto>> GetAllEngineersAsync();
    }
}
