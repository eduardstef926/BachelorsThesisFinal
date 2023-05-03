using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IUserService
    {
        Task AddUserSymptomsAsync(int cookieId, string symptoms);
        Task ScheduleAppointment(AppointmentDto appointment);
        Task AddAppointmentReviewAsync(ReviewDto review);
        Task AddUserSubscriptionAsync(SubscriptionInputDto subscriptionDto);
        Task UpdateSymptomsAsync();
        Task UpdateUserDataAsync(FullUserDataDto user);
        Task CancelUserSubscriptionAsync(int cookieId);
        Task<bool> CheckUserSubscriptionAsync(int cookieId);
        Task<SubscriptionDto> GetUserSubscriptionAsync(string email);
        Task<List<AppointmentDto>> GetUserAppointmentsByEmailAsync(string email);
        Task<FullUserDataDto> GetFullUserDataByCookieIdAsync(int cookieId);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<DiagnosticDto> GetLastDiagnosticBySessionIdAsync(int cookieId);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
