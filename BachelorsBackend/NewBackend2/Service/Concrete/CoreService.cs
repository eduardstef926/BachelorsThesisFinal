using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Helpers;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;
using Newtonsoft.Json;

namespace NewBackend2.Service.Concrete
{
    public class CoreService : ICoreService
    {
        private readonly IUserRepository userRepository;
        private readonly IMedicalRepository medicalRepository;
        private readonly IMapper mapper;

        public CoreService(IUserRepository userRepository, IMedicalRepository userSymptomMappingRepository, IMapper mapper)
        {
            this.medicalRepository = userSymptomMappingRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task UpdateSymptomsAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ApiHelper.baseUrl + "getAllSymptoms");
            var responseBody = await response.Content.ReadAsStringAsync();
            var symptomList = JsonConvert.DeserializeObject<string[]>(responseBody);
            foreach (var symptom in symptomList)
            {
                var newSymptom = new SymptomEntity
                {
                    Name = symptom,
                };
                await medicalRepository.AddSymptomAsync(newSymptom);
            }
        }

        public async Task<List<SymptomDto>> GetAllSymptomsAsync()
        {
            var symptoms = await medicalRepository.GetAllSymptomsAsync();
            return symptoms
                .Select(mapper.Map<SymptomEntity, SymptomDto>)
                .ToList();
        }

        public async Task AddUserSymptomsAsync(string userEmail, string symptoms)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ApiHelper.baseUrl + $"getInformationBySymptoms/{symptoms}");
            var resultBody = await response.Content.ReadAsStringAsync();
            resultBody = resultBody.Replace("\\", "").Replace("\n", "");
            var resultElements = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultBody);
            
            var disease = new DiseaseEntity
            {
                Name = resultElements["diseaseName"],  
            };
            await medicalRepository.AddDiseaseAsync(disease);

            var userId = await userRepository.GetUserIdByEmailAsync(userEmail);
            var userDiagnostic = new DiagnosticEntity
            {
                UserId = userId,
                DiseaseName = resultElements["diseaseName"],
                SymptomList = symptoms,
                DoctorTitle = resultElements["doctorTitle"],
                DoctorSpecialization = resultElements["doctorSpecialization"]
            };
            await medicalRepository.AddDiagnosticAsync(userDiagnostic);
        }

        public async Task<DiagnosticDto> GetLastDiagnosticByUserEmailAsync(string email)
        {
            var userId = await userRepository.GetUserIdByEmailAsync(email);
            var diagnostic = await medicalRepository.GetLastDiagnosticByUserIdAsync(userId);

            return mapper.Map<DiagnosticEntity, DiagnosticDto>(diagnostic);
        }
    }
}
