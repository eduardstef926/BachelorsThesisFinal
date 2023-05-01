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
        private readonly IEmailService emailService;
        private readonly IDoctorRepository doctorRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMedicalRepository medicalRepository;
        private readonly IMapper mapper;

        public UserService(ISubscriptionRepository subscriptionRepository, IEmailService emailService, IReviewRepository reviewRepository, IDoctorRepository doctorRepository, IUserRepository userRepository, IAppointmentRepository appointmentRepository, IMedicalRepository medicalRepository, IMapper mapper)
        {
            this.emailService = emailService;
            this.subscriptionRepository = subscriptionRepository;
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

        public async Task AddUserSubscriptionAsync(SubscriptionInputDto subscriptionDto)
        {
            var subscription = mapper.Map<SubscriptionInputDto, SubscriptionEntity>(subscriptionDto);
            var user = await userRepository.GetUserByEmailAsync(subscriptionDto.Email);

            subscription.UserId = user.UserId;
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = subscription.StartDate.AddMonths(subscriptionDto.Length);

            await this.emailService.SendSubscriptionPaymentAsync(user, subscription.EndDate);
            await subscriptionRepository.AddUserSubscriptionAsync(subscription);
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

        public async Task ScheduleAppointment(AppointmentDto appointment)
        {
            var user = await userRepository.GetUserByEmailAsync(appointment.UserEmail);
            var doctor = await doctorRepository.GetDoctorByFirstNameAndLastNameAsync(appointment.DoctorFirstName, appointment.DoctorLastName);
            
            var appointmentEntity = new AppointmentEntity
            {
                UserId = user.UserId,
                DoctorId = doctor.DoctorId,
                Price = appointment.Price,
                Location = appointment.Location,
                HospitalName = appointment.HospitalName,
                AppointmentDate = appointment.AppointmentDate.AddHours(3)
            };

            await appointmentRepository.AddAppointmentAsync(appointmentEntity);
            await emailService.SendAppointmentConfirmationEmailAsync(user, doctor, appointmentEntity);
        }

        public async Task<bool> CheckUserSubscriptionAsync(string email)
        {
            var subscription = await userRepository.GetUserSubscriptionAsync(email);
            
            if (subscription == null) 
                return false;

            return (DateTime.Now.CompareTo(subscription.StartDate) >= 0 && DateTime.Now.CompareTo(subscription.EndDate) <= 0);
        }

        public async Task<FullUserDataDto> GetFullUserDataByEmailAsync(string email)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            return mapper.Map<UserEntity, FullUserDataDto>(user);
        }

        public async Task UpdateUserDataAsync(FullUserDataDto user)
        {
            var updatedUser = mapper.Map<FullUserDataDto, UserEntity>(user);
            updatedUser.Password = await userRepository.GetUserPasswordByIdAsync(user.UserId);
            await userRepository.UpdateUserDataAsync(updatedUser); 
        }

        public async Task<List<AppointmentDto>> GetUserAppointmentsByEmailAsync(string email)
        {
            var appointments = await appointmentRepository.GetUserAppointmentsByEmailAsync(email);
            return appointments
                .Select(mapper.Map<AppointmentEntity, AppointmentDto>)
                .ToList();
        }

        public async Task<SubscriptionDto> GetUserSubscriptionAsync(string email)
        {
            var subscription = await userRepository.GetUserSubscriptionAsync(email);

            return mapper.Map<SubscriptionEntity, SubscriptionDto>(subscription);
        }
    }
}
