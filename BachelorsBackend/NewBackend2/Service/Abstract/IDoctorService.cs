using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IDoctorService
    {
        public Task AddDoctorAsync(DoctorDto doctor);
        public Task ScheduleAppointment(AppointmentDto appointment);
        public Task<List<DoctorDto>> GetAllDoctorsAsync();
        public Task<List<AppoimentSlotDto>> GetAppointmentDatesByDateSpecializationAndLocationAsync(string startDate, string endDate, string location, string specialization);
        public Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
        public Task<List<DegreeDto>> GetDoctorDegreeByFirstNameAndLastNameAsync(string firstName, string lastName);
        public Task<List<ReviewDto>> GetDoctorReviewsByFirstNameAndLastNameAsync(string firstName, string lastName);
        public Task<DoctorDto> GetDoctorWithEmploymentByFirstNameAndLastNameAsync(string firstName, string lastName);
        public Task<List<DoctorDto>> GetDoctorsBySpecialization(string specialization);
    }
}
