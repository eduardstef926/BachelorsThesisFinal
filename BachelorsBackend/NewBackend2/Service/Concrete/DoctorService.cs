using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;
using System.Globalization;

namespace NewBackend2.Service.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IEmailService emailService;
        private readonly IDegreeRepository degreeRepository;
        private readonly IEmploymentRepository employmentRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public DoctorService(IDoctorRepository doctorRepository, IEmailService emailService, IUserRepository userRepository, IAppointmentRepository appointmentRepository, IEmploymentRepository employmentRepository, 
            IReviewRepository reviewRepository, IDegreeRepository degreeRepository, IMapper mapper)
        {
            this.appointmentRepository = appointmentRepository;
            this.emailService = emailService;
            this.userRepository = userRepository;
            this.doctorRepository = doctorRepository;
            this.employmentRepository = employmentRepository;
            this.degreeRepository = degreeRepository;
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        public async Task AddDoctorAsync(DoctorDto doctor)
        {
            await doctorRepository.AddDoctorAsync(mapper.Map<DoctorDto, DoctorEntity>(doctor));
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await doctorRepository.GetAllDoctorsAsync();
            return doctors
                .Select(mapper.Map<DoctorEntity, DoctorDto>)
                .ToList();
        }

        public async Task<List<DegreeDto>> GetDoctorDegreeByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var degrees = await degreeRepository.GetDegreeByFirstNameAndLastNameAsync(firstName, lastName);
            return degrees
                .Select(mapper.Map<DegreeEntity, DegreeDto>)
                .ToList();
        }

        public async Task<DoctorDto> GetDoctorByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var doctor = await doctorRepository.GetDoctorByFirstNameAndLastNameAsync(firstName, lastName);
            return mapper.Map<DoctorEntity, DoctorDto>(doctor);
        }

        public async Task<List<ReviewDto>> GetDoctorReviewsByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var reviews = await reviewRepository.GetDoctorReviewsByFirstNameAndLastName(firstName, lastName);
            return reviews
                .Select(mapper.Map<ReviewEntity, ReviewDto>)
                .ToList();
        }

        public async Task<List<DoctorDto>> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await doctorRepository.GetDoctorsBySpecializationAsync(specialization);
            return doctors
                .Select(mapper.Map<DoctorEntity, DoctorDto>)
                .ToList();
        }

        public async Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization)
        {
            var locations = await employmentRepository.GetDoctorLocationsBySpecializationAsync(specialization);
            return locations;
        }

        public async Task<List<AppoimentSlotDto>> GetDoctorAppointmentsByDateAndLocationAsync(string startDate, string endDate, string location)
        {
            DateTime startTime = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan timeDifference = endTime - startTime;
            
            var appointmentSlotList = new List<AppoimentSlotDto>();
            int index = 0;
            
            while (index < timeDifference.TotalDays)
            {
                startTime = startTime.AddDays(1);
                if (!await appointmentRepository.CheckAppointmentDateAsync(startTime))
                {
                    var currentDay = startTime.DayOfWeek;
                    var appointmentSlots = await employmentRepository.GetAppointmentSlotsByDayAndLocationAsync(currentDay, location);
                    var appointmentSlotDto = appointmentSlots
                                            .Select(mapper.Map<EmploymentEntity, AppoimentSlotDto>)
                                            .ToList();
                    appointmentSlotDto.ForEach(x => x.Date = startTime);
                    appointmentSlotDto.ForEach(x => x.EndTime = x.StartTime.Add(TimeSpan.FromMinutes(30)));
                    appointmentSlotList.AddRange(appointmentSlotDto);
                    index += 1;
                }
            }

            return appointmentSlotList;
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
    }
}
