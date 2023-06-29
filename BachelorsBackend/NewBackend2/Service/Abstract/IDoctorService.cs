using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IDoctorService
    {
        Task AddDoctorAsync(DoctorDto doctor);
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<int> GetDoctorReviewNumbersByFirstNameAndLastName(string firstName, string lastName);
        Task<List<AppoimentSlotDto>> GetAppointmentDatesByDateSpecializationAndLocationAsync(string startDate, string endDate, string location, string specialization);
        Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
        Task<List<int>> GetDoctorReviewNumberByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<DegreeDto>> GetDoctorDegreesByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<ReviewDto>> GetDoctorReviewsPaginatedByFirstNameAndLastNameAsync(string firstName, string lastName, int pageIndex);
        Task<DoctorDto> GetDoctorWithEmploymentByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<DoctorDto>> GetDoctorsBySpecialization(string specialization);
    }
}
