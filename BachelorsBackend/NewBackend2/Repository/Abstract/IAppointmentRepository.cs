using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IAppointmentRepository
    {
        Task AddAppointmentAsync(AppointmentEntity appointment);
        Task UpdateAppointmentReviewStatusAsync(int appointmentId); 
        Task<bool> CheckAppointmentDateAsync(DateTime appointmentDate);
        Task<List<AppointmentEntity>> GetUserAppointmentsByEmailAsync(string email);
        Task<List<AppointmentEntity>> GetFullAppointmentsDataAsync();
        Task<AppointmentEntity> GetAppointmentByDateAsync(DateTime date);
        Task<AppointmentEntity> GetAppointmentByIdAsync(int id);
    }
}
