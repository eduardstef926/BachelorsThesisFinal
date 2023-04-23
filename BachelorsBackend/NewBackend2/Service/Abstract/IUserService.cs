using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IUserService
    {
        Task AddUserSymptomsAsync(string userEmail, string symptoms);
        Task AddAppointmentReviewAsync(ReviewDto review);
        Task UpdateSymptomsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<DiagnosticDto> GetLastDiagnosticByUserEmailAsync(string email);
        Task<List<SymptomDto>> GetAllSymptomsAsync();
    }
}
