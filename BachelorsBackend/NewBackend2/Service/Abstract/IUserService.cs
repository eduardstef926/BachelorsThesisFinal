using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IUserService
    {
        Task AddUserSymptomsAsync(string userEmail, string symptoms);
        Task ScheduleAppointment(AppointmentDto appointment);
        Task<List<AppointmentDto>> GetUserAppointmentsByEmailAsync(string email);
        Task AddAppointmentReviewAsync(ReviewDto review);
        Task<bool> CheckUserSubscriptionAsync(string email);
        Task<SubscriptionDto> GetUserSubscriptionAsync(string email);
        Task AddUserSubscriptionAsync(SubscriptionInputDto subscriptionDto);
        Task UpdateSymptomsAsync();
        Task UpdateUserDataAsync(FullUserDataDto user);
        Task<FullUserDataDto> GetFullUserDataByEmailAsync(string email);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<DiagnosticDto> GetLastDiagnosticByUserEmailAsync(string email);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
