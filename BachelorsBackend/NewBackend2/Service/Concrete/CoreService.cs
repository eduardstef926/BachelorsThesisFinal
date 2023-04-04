using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class CoreService : ICoreService
    {
        private readonly ISymptomRespository symptomRespository;
        private readonly IUserRepository userRepository;

        public CoreService(ISymptomRespository symptomRespository, IUserRepository userRepository)
        {
            this.symptomRespository = symptomRespository;
            this.userRepository = userRepository;
        }

        public async Task<string> GetSymptomDataAsync(string userEmail, List<string> symptomList)
        {
            var userId = await userRepository.GetUserIdByEmailAsync(userEmail);
            foreach (var symptom in symptomList)
            {
                var userSymptom = new SymptomEntity
                {
                    Symptom = symptom,
                    UserId = userId,    
                };
                await symptomRespository.AddSymptomAsync(userSymptom);
            }
            return null;
        }
    }
}
