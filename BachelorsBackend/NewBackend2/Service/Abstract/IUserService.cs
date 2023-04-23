using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IUserService
    {
        Task AddUserSymptomsAsync(string userEmail, string symptoms);
        Task ScheduleAppointment(AppointmentDto appointment);
        Task AddAppointmentReviewAsync(ReviewDto review);
        Task<bool> CheckUserSubscriptionAsync(string email);
        Task AddUserSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task UpdateSymptomsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<DiagnosticDto> GetLastDiagnosticByUserEmailAsync(string email);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
