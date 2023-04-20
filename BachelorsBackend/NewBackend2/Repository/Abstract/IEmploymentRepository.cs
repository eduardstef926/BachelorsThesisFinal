using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IEmploymentRepository
    {
        Task<List<EmploymentEntity>> GetAppointmentSlotsByDayAndLocationAsync(DayOfWeek day, string location);   
        Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
    }
}
