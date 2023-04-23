using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IEmploymentRepository
    {
        Task<List<EmploymentEntity>> GetAppointmentDatesByDateSpecializationAndLocation(DayOfWeek day, string location, string specialization);
        Task<EmploymentEntity> GetEmploymentByDoctorIdAsync(int doctorId);
        Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
        Task<List<string>> GetDoctorLocationsByDoctorId(int doctorId);
    }
}
