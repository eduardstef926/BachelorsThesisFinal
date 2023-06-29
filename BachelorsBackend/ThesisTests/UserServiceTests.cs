using AutoMapper;
using Moq;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;
using NewBackend2.Service.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThesisTests
{
    public class UserServiceTests
    {
        private UserService userService;
        private Mock<IUserRepository> userRepository;
        private Mock<IEmailService> emailService;
        private Mock<IDoctorRepository> doctorRepository;
        private Mock<IReviewRepository> reviewRepository;
        private Mock<ICookieRepository> cookieRepository;
        private Mock<ISubscriptionRepository> subscriptionRepository;
        private Mock<IAppointmentRepository> appointmentRepository;
        private Mock<IMedicalRepository> medicalRepository;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<DoctorEntity, DoctorDto>();
                mc.CreateMap<DoctorDto, DoctorEntity>();
                mc.CreateMap<UserEntity, UserDto>();
                mc.CreateMap<UserEntity, FullUserDataDto>();
                mc.CreateMap<FullUserDataDto, UserEntity>();
                mc.CreateMap<UserDto, UserEntity>();
                mc.CreateMap<ReviewEntity, ReviewDto>();
                mc.CreateMap<DiseaseEntity, DiseaseDto>();
                mc.CreateMap<DiagnosisEntity, DiagnosisDto>();
                mc.CreateMap<HospitalEntity, HospitalDto>();
                mc.CreateMap<SubscriptionInputDto, SubscriptionEntity>();
                mc.CreateMap<SubscriptionEntity, SubscriptionDto>();
                mc.CreateMap<ReviewEntity, ReviewDto>();
                mc.CreateMap<SymptomEntity, SymptomDto>()
                    .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));
                mc.CreateMap<ReviewDto, ReviewEntity>();
                mc.CreateMap<DegreeEntity, DegreeDto>()
                    .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.College.Name))
                    .ForMember(dto => dto.Location, opt => opt.MapFrom(src => src.College.Location))
                    .ForMember(dto => dto.StartYear, opt => opt.MapFrom(src => src.StartYear))
                    .ForMember(dto => dto.EndYear, opt => opt.MapFrom(src => src.EndYear))
                    .ForMember(dto => dto.StudyProgram, opt => opt.MapFrom(src => src.StudyProgram));
                mc.CreateMap<EmploymentEntity, AppoimentSlotDto>()
                    .ForMember(dto => dto.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                    .ForMember(dto => dto.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                    .ForMember(dto => dto.Location, opt => opt.MapFrom(src => src.Hospital.Location))
                    .ForMember(dto => dto.HospitalName, opt => opt.MapFrom(src => src.HospitalName))
                    .ForMember(dto => dto.StartTime, opt => opt.MapFrom(src => src.StartTime))
                    .ForMember(dto => dto.EndTime, opt => opt.MapFrom(src => src.EndTime))
                    .ForMember(dto => dto.Rating, opt => opt.MapFrom(src => src.Doctor.Rating))
                    .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ConsultPrice));
                mc.CreateMap<AppointmentEntity, AppointmentDto>()
                    .ForMember(dto => dto.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                    .ForMember(dto => dto.LastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                    .ForMember(dto => dto.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate));
            });

            mapper = mapperConfig.CreateMapper();
            userRepository = new Mock<IUserRepository>();
            emailService = new Mock<IEmailService>();
            doctorRepository = new Mock<IDoctorRepository>();
            reviewRepository = new Mock<IReviewRepository>();
            cookieRepository = new Mock<ICookieRepository>();
            subscriptionRepository = new Mock<ISubscriptionRepository>();
            appointmentRepository = new Mock<IAppointmentRepository>();
            medicalRepository = new Mock<IMedicalRepository>();
            userService = new UserService(subscriptionRepository.Object, cookieRepository.Object, emailService.Object, reviewRepository.Object, doctorRepository.Object, userRepository.Object, appointmentRepository.Object, medicalRepository.Object, mapper);
        }

        [Test]
        public async Task AddUserSubscriptionTest()
        {
            int cookieId = 1;
            int length = 10;
            var subscriptionInput = new SubscriptionInputDto
            {
                CookieId = cookieId,
                Length = length,
            };
            var subscription = new SubscriptionEntity
            {
                UserId = 1,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(length),
            };
            var userEntity = new UserEntity
            {
                FirstName = "Aurel",
                LastName = "Tudor",
                PhoneNumber = "072325023",
                Password = "1234",
                Email = "aurelTudor@gmail.com",
                isEmailConfirmed = false
            };

            cookieRepository.Setup(r => r.GetUserByCookieIdAsync(cookieId)).ReturnsAsync(userEntity);

            //Act
            await userService.AddUserSubscriptionAsync(subscriptionInput);

            emailService.Verify(x => x.SendSubscriptionPaymentAsync(userEntity, subscription.EndDate), Times.Once);
            subscriptionRepository.Verify(x => x.AddUserSubscriptionAsync(subscription), Times.Once);
        }

        [Test]
        public async Task GetLastDiagnosticBySessionIdTest()
        {
            int cookieId = 1;
            int userId = 1;
            var diagnosis = new DiagnosisEntity
            {
                UserId = 1,
                DiseaseName = "Fungal Infection",
                DoctorTitle = "Dermatologist",
                DoctorSpecialization = "dermatology",
            };
            var diagnosisDto = new DiagnosisDto
            {
                DiseaseName = "Fungal Infection",
                DoctorTitle = "Dermatologist",
                DoctorSpecialization = "dermatology",
            };

            cookieRepository.Setup(r => r.GetUserIdByCookieIdAsync(cookieId)).ReturnsAsync(userId);
            medicalRepository.Setup(r => r.GetLastDiagnosticByUserIdAsync(userId)).ReturnsAsync(diagnosis);

            var result = await userService.GetLastDiagnosticBySessionIdAsync(userId);

            Assert.AreEqual(result.DiseaseName, diagnosisDto.DiseaseName);
            Assert.AreEqual(result.DoctorTitle, diagnosisDto.DoctorTitle);
            Assert.AreEqual(result.DoctorSpecialization, diagnosisDto.DoctorSpecialization);
            cookieRepository.Verify(r => r.GetUserIdByCookieIdAsync(cookieId), Times.Once);
            medicalRepository.Verify(r => r.GetLastDiagnosticByUserIdAsync(userId), Times.Once);
        }

        [Test]
        public async Task AddAppointmentReviewTest()
        {
            int appointmentId = 1;
            int doctorId = 1;
            var reviewNumbers = new List<int> { 1, 2, 3 };
            int evaluationNumber = 2;
            var appointment = new AppointmentEntity
            {
                AppointmentId = appointmentId,
                UserId = 1,
                DoctorId = doctorId,
                AppointmentDate = DateTime.Now,
                Location = "Cluj-Napoca",
                HospitalName = "Constantin-Opris",
                Price = 100,
                IsReviewed = false,
            };
            var reviewDto = new ReviewDto
            {
                CookieId = 1,
                AppointmentId = appointmentId,
                Number = 10,
                Message = "It was a pleasure"
            };
            var reviewEntity = new ReviewEntity
            {
                AppointmentId = 1,
                Number = 10,
                Message = "It was a pleasure",
            };
            var doctor = new DoctorEntity
            {
                DoctorId = doctorId,
                FirstName = "Aurel",
                LastName = "Tudor",
                Email = "aurelTudor@gmail.com",
                Specialization = "general medicine",
                Rating = 10
            };
            appointment.Doctor = doctor;

            appointmentRepository.Setup(x => x.GetAppointmentByIdAsync(appointmentId)).ReturnsAsync(appointment);
            reviewRepository.Setup(x => x.GetDoctorReviewNumbersByFirstNameAndLastName("Aurel", "Tudor")).ReturnsAsync(reviewNumbers);

            await userService.AddAppointmentReviewAsync(reviewDto);

            doctorRepository.Verify(x => x.UpdateDoctorEvaluationNumberAsync(doctorId,evaluationNumber), Times.Once);
            appointmentRepository.Verify(x => x.UpdateAppointmentReviewStatusAsync(appointmentId), Times.Once);
            reviewRepository.Verify(x => x.AddAppointmentReviewAsync(reviewEntity), Times.Once);
        }

        [Test]
        public async Task ScheduleAppointmentTest()
        {
            int userId = 10;
            string firstName = "Aurel";
            string lastName = "Tudor";
            var appointmentDto = new AppointmentDto
            {
                AppointmentId = 1,
                FirstName = firstName,
                LastName = lastName,
                CookieId = 1,
                HospitalName = "Constantin-Opris",
                Location = "Cluj-Napoca",
                AppointmentDate = DateTime.Now,
                Price = 100,
                IsReviewed = false
            };
            var doctor = new DoctorEntity
            {
                DoctorId = 1,
                FirstName = "Andrei",
                LastName = "Filip",
                Email = "andreiFilip@gmail.com",
                Specialization = "general medicine",
                Rating = 10,
            };

            var user = new UserEntity
            {
                UserId = userId,
                FirstName = "Tudor",
                LastName = "Andrei",

            };

            var appointmentEntity = new AppointmentEntity
            {
                UserId = userId,
                DoctorId = doctor.DoctorId,
                Price = appointmentDto.Price,
                Location = appointmentDto.Location,
                HospitalName = appointmentDto.HospitalName,
                AppointmentDate = appointmentDto.AppointmentDate,
            };

            cookieRepository.Setup(x => x.GetUserByCookieIdAsync(appointmentDto.CookieId)).ReturnsAsync(user);
            doctorRepository.Setup(x => x.GetDoctorByFirstNameAndLastNameAsync(firstName, lastName)).ReturnsAsync(doctor);

            await userService.ScheduleAppointment(appointmentDto);

            appointmentRepository.Verify(x => x.AddAppointmentAsync(appointmentEntity), Times.Once);
            emailService.Verify(x => x.SendAppointmentConfirmationEmailAsync(user, doctor, appointmentEntity), Times.Once);
        }
    }
}
