using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IAppointmentRepository
    {
        Task AddAppointmentAsync(AppointmentEntity appointment);
        Task<AppointmentEntity> GetAppointmentByDateAsync(DateTime date);
    }
}
