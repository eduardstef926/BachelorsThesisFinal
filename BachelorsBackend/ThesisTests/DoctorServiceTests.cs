using AutoMapper;
using Moq;
using NewBackend2.Dtos;
using NewBackend2.Enums;
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
    public class Tests
    {
        private DoctorService doctorService;
        private Mock<IDoctorRepository> doctorRepository;
        private Mock<IAppointmentRepository> appointmentRepository;
        private Mock<IDegreeRepository> degreeRepository;
        private Mock<IEmploymentRepository> employmentRepository;
        private Mock<IReviewRepository> reviewRepository;
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
            doctorRepository = new Mock<IDoctorRepository>();
            appointmentRepository = new Mock<IAppointmentRepository>();
            degreeRepository = new Mock<IDegreeRepository>();
            employmentRepository = new Mock<IEmploymentRepository>();
            reviewRepository = new Mock<IReviewRepository>();
            doctorService = new DoctorService(doctorRepository.Object, appointmentRepository.Object,
                                              employmentRepository.Object, reviewRepository.Object,
                                              degreeRepository.Object, mapper);
        }

        public DoctorEntity CreateDoctorEntity(int doctorId, string firstName, string lastName, string email, string specialization, float rating)
        {
            var doctor = new DoctorEntity
            {
                DoctorId = doctorId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Specialization = specialization,
                Rating = rating
            };
            return doctor;
        }

        [Test]
        public async Task GetAllDoctorsTest()
        {
            var location = "Baia Mare";
            var doctorDtoList = new List<DoctorDto>();
            var doctorList = new List<DoctorEntity>();
            var doctor1 = CreateDoctorEntity(1, "Andrei", "Tudor", "eddistef@gmail.com", "surgery", 10);
            var doctor2 = CreateDoctorEntity(2, "Filip", "Andrei", "tudorAndrei@gmail.com", "general medicine", 8);
            var doctorDto1 = mapper.Map<DoctorEntity, DoctorDto>(doctor1);
            var doctorDto2 = mapper.Map<DoctorEntity, DoctorDto>(doctor2);
            doctorDto1.Location = location;
            doctorDto2.Location = location;

            doctorList.Add(doctor1);
            doctorList.Add(doctor2);
            doctorDtoList.Add(doctorDto1);
            doctorDtoList.Add(doctorDto2);

            doctorRepository.Setup(r => r.GetAllDoctorsAsync()).ReturnsAsync(doctorList);
            employmentRepository.Setup(r => r.GetFirstDoctorLocationsByDoctorId(1)).ReturnsAsync(location);
            employmentRepository.Setup(r => r.GetFirstDoctorLocationsByDoctorId(2)).ReturnsAsync(location);

            // Act
            var results = await doctorService.GetAllDoctorsAsync();

            // Assert
            Assert.AreEqual(doctorDtoList.Count, results.Count);
            for (int i = 0; i < results.Count; i++)
            {
                employmentRepository.Verify(x => x.GetFirstDoctorLocationsByDoctorId(i + 1), Times.Once);
                Assert.AreEqual(results[i], doctorDtoList[i]);
            }
            doctorRepository.Verify(x => x.GetAllDoctorsAsync(), Times.Once);
        }


        [Test]
        public async Task GetDoctorsBySpecializationTest()
        {
            var specialization = "general medicine";
            var doctorEntity = new DoctorEntity
            {
                FirstName = "Aurel",
                LastName = "Tudor",
                Email = "aurelTudor@gmail.com",
                Specialization = specialization
            };
            var doctorDto = new DoctorDto
            {
                FirstName = "Aurel",
                LastName = "Tudor",
                Email = "aurelTudor@gmail.com",
                Specialization = specialization,
                Location = null,
                Rating = 0
            };

            var doctorList = new List<DoctorEntity>() { doctorEntity };

            doctorRepository.Setup(r => r.GetDoctorsBySpecializationAsync(specialization)).ReturnsAsync(doctorList);

            //Act
            var results = await doctorService.GetDoctorsBySpecialization(specialization);

            // Assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(doctorDto, results[0]);
            doctorRepository.Verify(x => x.GetDoctorsBySpecializationAsync(specialization), Times.Once);
        }

        [Test]
        public async Task GetDoctorDegreesByFirstNameAndLastNameTest()
        {
            var firstName = "Aurel";
            var lastName = "Tudor";
            var startYear = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var endYear = new DateTime(DateTime.Now.AddYears(1).Year, DateTime.Now.Month, DateTime.Now.Day);

            var degree = new DegreeEntity
            {
                DoctorId = 1,
                CollegeId = 1,
                StartYear = startYear,
                EndYear = endYear,
                StudyField = StudyField.AdvancedSurgery,
                StudyProgram = StudyProgram.BachelorsDegree
            };
            var degreeList = new List<DegreeEntity>() { degree };
            var degreeDto = new DegreeDto
            {
                Name = null,
                StartYear = startYear,
                EndYear = endYear,
                StudyField = StudyField.AdvancedSurgery,
                StudyProgram = StudyProgram.BachelorsDegree
            };

            degreeRepository.Setup(r => r.GetDegreesByFirstNameAndLastNameAsync(firstName, lastName)).ReturnsAsync(degreeList);

            //Act
            var results = await doctorService.GetDoctorDegreesByFirstNameAndLastNameAsync(firstName, lastName);

            // Assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(degreeDto.StartYear, results[0].StartYear);
            Assert.AreEqual(degreeDto.EndYear, results[0].EndYear);
            Assert.AreEqual(degreeDto.StudyField, results[0].StudyField);
            Assert.AreEqual(degreeDto.StudyProgram, results[0].StudyProgram);
            degreeRepository.Verify(x => x.GetDegreesByFirstNameAndLastNameAsync(firstName, lastName), Times.Once);
        }


        [Test]
        public async Task GetDoctorReviewsPaginatedByFirstNameAndLastNameTest()
        {
            var firstName = "Aurel";
            var lastName = "Tudor";
            int pageIndex = 1;
            var reviewEntity = new ReviewEntity
            {
                AppointmentId = 1,
                Number = 10,
                Message = "It was a pleasure"
            };
            var reviewList = new List<ReviewEntity>() { reviewEntity };
            var reviewDto = new ReviewDto
            {
                AppointmentId = 1,
                Message = "It was a pleasure",
                Number = 10,
            };

            reviewRepository.Setup(r => r.GetDoctorReviewsPaginatedByFirstNameAndLastName(firstName, lastName, pageIndex)).ReturnsAsync(reviewList);

            //Act
            var results = await doctorService.GetDoctorReviewsPaginatedByFirstNameAndLastNameAsync(firstName, lastName, pageIndex);

            // Assert
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0].AppointmentId, reviewDto.AppointmentId);
            Assert.AreEqual(results[0].Number, reviewDto.Number);
            Assert.AreEqual(results[0].Message, reviewDto.Message);
            reviewRepository.Verify(x => x.GetDoctorReviewsPaginatedByFirstNameAndLastName(firstName, lastName, pageIndex), Times.Once);
        }
    }
}