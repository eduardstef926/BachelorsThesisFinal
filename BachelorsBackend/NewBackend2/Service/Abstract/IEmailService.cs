using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailEntity email);
        Task SendSubscriptionPaymentAsync(UserEntity user, DateTime endDate);
        Task SendAppointmentConfirmationEmailAsync(UserEntity user, DoctorEntity doctor, AppointmentEntity appointment);
        Task SendForgotPasswordEmailAsync(string userEmail);
        Task SendWelcomeEmailAsync(string firstName, string lastName);
        Task SendAppointmentReminderAsync();
        Task SendAppointmentFeedbackEmailAsync();
    }
}
