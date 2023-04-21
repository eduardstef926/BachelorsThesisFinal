using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IEmploymentRepository
    {
        Task<List<EmploymentEntity>> GetAppointmentDatesByDateSpecializationAndLocation(DayOfWeek day, string location, string specialization);   
        Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
    }
}
