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

        public async Task<List<AppoimentSlotDto>> GetAppointmentDatesByDateSpecializationAndLocationAsync(string startInputDate, string endInputDate, string location, string specialization)
        {
            var startDate = DateTime.ParseExact(startInputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(endInputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dayDifference = endDate - startDate;
            var appointmentSlotList = new List<AppoimentSlotDto>();
            int index = 0;
            
            while (index < dayDifference.TotalDays)
            {
                startDate = startDate.AddDays(1);
                var currentDay = startDate.DayOfWeek;
                var appointmentSlots = await employmentRepository.GetAppointmentDatesByDateSpecializationAndLocation(currentDay, location, specialization);
                var appointmentSlotMappings = appointmentSlots
                    .Select(mapper.Map<EmploymentEntity, AppoimentSlotDto>)
                    .ToList();
                
                foreach (var appointment in appointmentSlotMappings)
                {
                    var startHour = appointment.StartTime;
                    var newDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour.Hours, startHour.Minutes, startHour.Seconds);
                    var maximumEndHour = appointment.EndTime.Subtract(TimeSpan.FromMinutes(30));
                    var hourDifference = maximumEndHour - startHour;
                    while (hourDifference.TotalMinutes > 0)
                    {
                        if (!await appointmentRepository.CheckAppointmentDateAsync(newDate))
                        {
                            appointment.Date = newDate;
                            appointment.StartTime = startHour;
                            appointment.EndTime = startHour.Add(TimeSpan.FromMinutes(30));
                            appointmentSlotList.Add(appointment);
                            break;
                        }

                        startHour = startHour.Add(TimeSpan.FromMinutes(30));
                        newDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour.Hours, startHour.Minutes, startHour.Seconds);
                        hourDifference = maximumEndHour - startHour;
                    }
                }

                index += 1;
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
