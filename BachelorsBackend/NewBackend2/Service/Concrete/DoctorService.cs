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
        private readonly IDegreeRepository degreeRepository;
        private readonly IEmploymentRepository employmentRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public DoctorService(IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IEmploymentRepository employmentRepository, 
            IReviewRepository reviewRepository, IDegreeRepository degreeRepository, IMapper mapper)
        {
            this.appointmentRepository = appointmentRepository;
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
            var doctorDtos = new List<DoctorDto>();

            for (int i=0; i< doctors.Count; i++)
            {
                var doctorDto = mapper.Map<DoctorEntity, DoctorDto>(doctors[i]);
                doctorDto.Location = await employmentRepository.GetFirstDoctorLocationsByDoctorId(doctors[i].DoctorId);
                doctorDtos.Add(doctorDto);
            }

            return doctorDtos;
        }

        public async Task<List<DegreeDto>> GetDoctorDegreesByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var degrees = await degreeRepository.GetDegreesByFirstNameAndLastNameAsync(firstName, lastName);
            return degrees
                .Select(mapper.Map<DegreeEntity, DegreeDto>)
                .ToList();
        }

        public async Task<DoctorDto> GetDoctorWithEmploymentByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var doctor = await doctorRepository.GetDoctorByFirstNameAndLastNameAsync(firstName, lastName);
            var employment = await employmentRepository.GetEmploymentByDoctorIdAsync(doctor.DoctorId);
            var doctorDto = mapper.Map<DoctorEntity, DoctorDto>(doctor);

            doctorDto.CurrentPosition = employment.CurrentPosition;
            doctorDto.HospitalName = employment.HospitalName;
            doctorDto.Location = employment.Hospital.Location;

            return doctorDto;
        }

        public async Task<List<ReviewDto>> GetDoctorReviewsPaginatedByFirstNameAndLastNameAsync(string firstName, string lastName, int pageIndex)
        {
            var reviews = await reviewRepository.GetDoctorReviewsPaginatedByFirstNameAndLastName(firstName, lastName, pageIndex);
            return reviews
                .Select(mapper.Map<ReviewEntity, ReviewDto>)
                .ToList();
        }

        public async Task<int> GetDoctorReviewNumbersByFirstNameAndLastName(string firstName, string lastName)
        {
            return await reviewRepository.GetDoctorReviewLengthByFirstNameAndLastName(firstName, lastName);
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
            return await employmentRepository.GetDoctorLocationsBySpecializationAsync(specialization);
        }

        public async Task<List<AppoimentSlotDto>> GetAppointmentDatesByDateSpecializationAndLocationAsync(string startInputDate, string endInputDate,
                                                                                                          string location, string specialization)
        {
            var startDate = DateTime.ParseExact(startInputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(endInputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dayDifference = endDate - startDate;
            var appointmentSlotList = new List<AppoimentSlotDto>();
            int index = 0;
            
            while (index < dayDifference.TotalDays)
            {
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
                if ((int) startDate.DayOfWeek == 5)
                {
                    startDate = startDate.AddDays(3);
                    index += 3;
                }
                else
                {
                    startDate = startDate.AddDays(1);
                    index += 1;
                }
            }
            return appointmentSlotList;
        }

        public async Task<List<int>> GetDoctorReviewNumberByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            return await reviewRepository.GetDoctorReviewNumbersByFirstNameAndLastName(firstName, lastName);
        }
    }
}
