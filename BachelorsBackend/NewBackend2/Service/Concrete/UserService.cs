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
        private readonly ICookieRepository cookieRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMedicalRepository medicalRepository;
        private readonly IMapper mapper;

        public UserService(ISubscriptionRepository subscriptionRepository, ICookieRepository cookieRepository, IEmailService emailService, IReviewRepository reviewRepository, IDoctorRepository doctorRepository, IUserRepository userRepository, IAppointmentRepository appointmentRepository, IMedicalRepository medicalRepository, IMapper mapper)
        {
            this.emailService = emailService;
            this.cookieRepository = cookieRepository;
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

        public async Task AddUserSymptomsAsync(int cookieId, string symptoms)
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

            var userId = await cookieRepository.GetUserIdByCookieIdAsync(cookieId);
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
            var user = await cookieRepository.GetUserByCookieIdAsync(subscriptionDto.CookieId);

            subscription.UserId = user.UserId;
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = subscription.StartDate.AddMonths(subscriptionDto.Length);

            await this.emailService.SendSubscriptionPaymentAsync(user, subscription.EndDate);
            await subscriptionRepository.AddUserSubscriptionAsync(subscription);
        }

        public async Task<DiagnosticDto> GetLastDiagnosticBySessionIdAsync(int cookieId)
        {
            var userId = await cookieRepository.GetUserIdByCookieIdAsync(cookieId);
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
            var appointment = await appointmentRepository.GetAppointmentByIdAsync(review.AppointmentId);
            var reviewNumbers = await reviewRepository.GetDoctorReviewNumbersByFirstNameAndLastName(appointment.Doctor.FirstName, appointment.Doctor.LastName);
            var reviewEntity = mapper.Map<ReviewDto, ReviewEntity>(review);
            int evaluationSum = 0; float evaluationNumber;
            
            if (reviewNumbers.Count > 0)
            {
                reviewNumbers.ForEach(reviewNumber => evaluationSum += reviewNumber);
                var evaluationAverage = (float)evaluationSum / reviewNumbers.Count();
                evaluationNumber = (float)Math.Round(evaluationAverage, 2);
            } 
            else
            {
                evaluationNumber = review.Number;
            }

            await doctorRepository.UpdateDoctorEvaluationNumberAsync(appointment.Doctor.DoctorId, evaluationNumber);
            await appointmentRepository.UpdateAppointmentReviewStatusAsync(appointment.AppointmentId);
            await reviewRepository.AddAppointmentReviewAsync(reviewEntity);
        }

        public async Task ScheduleAppointment(AppointmentDto appointment)
        {
            var userId = await cookieRepository.GetUserByCookieIdAsync(appointment.CookieId);
            var doctor = await doctorRepository.GetDoctorByFirstNameAndLastNameAsync(appointment.DoctorFirstName, appointment.DoctorLastName);
            
            var appointmentEntity = new AppointmentEntity
            {
                UserId = userId.UserId,
                DoctorId = doctor.DoctorId,
                Price = appointment.Price,
                Location = appointment.Location,
                HospitalName = appointment.HospitalName,
                AppointmentDate = appointment.AppointmentDate.AddHours(3)
            };

            await appointmentRepository.AddAppointmentAsync(appointmentEntity);
            await emailService.SendAppointmentConfirmationEmailAsync(userId, doctor, appointmentEntity);
        }

        public async Task<bool> CheckUserSubscriptionAsync(int cookieId)
        {
            var userId = await cookieRepository.GetUserIdByCookieIdAsync(cookieId);
            return await subscriptionRepository.CheckUserSubscriptionAsync(userId);
        }

        public async Task<FullUserDataDto> GetFullUserDataByCookieIdAsync(int cookieId)
        {
            var user = await cookieRepository.GetUserByCookieIdAsync(cookieId);
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
            var subscription = await subscriptionRepository.GetUserSubscriptionAsync(email);
            return mapper.Map<SubscriptionEntity, SubscriptionDto>(subscription);
        }

        public async Task CancelUserSubscriptionAsync(int cookieId)
        {
            var userId = await cookieRepository.GetUserIdByCookieIdAsync(cookieId);
            var user = await userRepository.GetUserByUserIdAsync(userId);

            await emailService.SendSubscriptionCancelAsync(user);
            await subscriptionRepository.DeleteUserSubscriptionAsync(userId);
        }

        public async Task<PaginatedSymptomDto> FilterSymptomsAsync(string? symptom, int pageIndex)
        {
            int number, pageStart = 5 * pageIndex;
            List<string> symptoms;

            if (symptom == null)
            {
                symptoms = await medicalRepository.GetAllSymptomsInRangeAsync(pageStart);
                number = await medicalRepository.GetSymptomsNumberAsync();
            }
            else
            {
                symptoms = await medicalRepository.FilterSymptomsAsync(symptom, pageStart);
                number = await medicalRepository.GetSymptomsNumberAsync(symptom);
            }

            var paginatedSymptoms = new PaginatedSymptomDto
            {
                Symptoms = symptoms,
                Number = number,
            };

            return paginatedSymptoms;
        }
    }
}
