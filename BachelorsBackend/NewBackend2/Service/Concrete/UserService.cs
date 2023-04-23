using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Helpers;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;
using Newtonsoft.Json;

namespace NewBackend2.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMedicalRepository medicalRepository;
        private readonly IMapper mapper;

        public UserService(IReviewRepository reviewRepository, IDoctorRepository doctorRepository, IUserRepository userRepository, IAppointmentRepository appointmentRepository, IMedicalRepository medicalRepository, IMapper mapper)
        {
            this.doctorRepository = doctorRepository;
            this.reviewRepository = reviewRepository;
            this.appointmentRepository = appointmentRepository;
            this.medicalRepository = medicalRepository;
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

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await appointmentRepository.GetAppointmentByIdAsync(id);
            return mapper.Map<AppointmentEntity, AppointmentDto>(appointment);
        }

        public async Task AddAppointmentReviewAsync(ReviewDto review)
        {
            var reviewNumbers = await reviewRepository.GetDoctorReviewNumbersByFirstNameAndLastName(review.DoctorFirstName, review.DoctorLastName);
            var userId = await userRepository.GetUserIdByEmailAsync(review.UserEmail);
            var doctorId = await doctorRepository.GetDoctorIdByFirstNameAndLastNameAsync(review.DoctorFirstName, review.DoctorLastName);
            var reviewEntity = mapper.Map<ReviewDto, ReviewEntity>(review);
            int evaluationSum = 0;
            
            reviewNumbers.ForEach(reviewNumber => evaluationSum += reviewNumber);
            var evaluationAverage = (float) evaluationSum / reviewNumbers.Count();
            var evaluationNumber = (float) Math.Round(evaluationAverage, 2);

            await doctorRepository.UpdateDoctorEvaluationNumberAsync(doctorId, evaluationNumber);

            reviewEntity.DoctorId = doctorId;
            reviewEntity.UserId = userId;
            await reviewRepository.AddAppointmentReviewAsync(reviewEntity);
        }
    }
}
