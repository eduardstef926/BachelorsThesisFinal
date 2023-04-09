using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class CoreService : ICoreService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserSymptomMappingRepository userSymptomMappingRepository;
        private readonly IMapper mapper;

        public CoreService(IUserRepository userRepository, IUserSymptomMappingRepository userSymptomMappingRepository, IMapper mapper)
        {
            this.userSymptomMappingRepository = userSymptomMappingRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<List<SymptomDto>> GetAllSymptomsAsync()
        {
            var symptoms = await userSymptomMappingRepository.GetAllSymptomsAsync();
            return symptoms
                .Select(mapper.Map<SymptomEntity, SymptomDto>)
                .ToList();
        }

        public async Task AddUserSymptomsAsync(string userEmail, List<string> symptomNameList)
        {
            foreach (var symptomName in symptomNameList)
            {
                var userId = await userRepository.GetUserIdByEmailAsync(userEmail);
                var userSymptom = new UserSymptomMapping
                {
                    UserId = userId,
                    SymptomName = symptomName
                };
                await userSymptomMappingRepository.AddUserSymptomMappingAsync(userSymptom);
            }
        }
    }
}
