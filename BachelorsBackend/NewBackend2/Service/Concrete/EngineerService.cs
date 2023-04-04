using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class EngineerService : IEngineerService
    {
        private readonly IEngineerRepository engineerRepository;
        private readonly IMapper mapper;

        public EngineerService(IEngineerRepository engineerRepository, IMapper mapper)
        {
            this.engineerRepository = engineerRepository;
            this.mapper = mapper;
        }

        public async Task AddEngineerAsync(EngineerDto engineer)
        {
            await engineerRepository.AddEngineerAsync(mapper.Map<EngineerDto, EngineerEntity>(engineer));
        }

        public async Task<List<EngineerDto>> GetAllEngineersAsync()
        {
            var engineers = await engineerRepository.GetAllEngineersAsync();
            return engineers
                .Select(mapper.Map<EngineerEntity, EngineerDto>)
                .ToList();
        }
    }
}
