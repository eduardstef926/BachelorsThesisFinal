using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailEntity email);
        Task SendEmailConfirmationAsync(string userEmail);
        Task SendSubscriptionPaymentAsync(UserEntity user, DateTime endDate);
        Task SendSubscriptionCancelAsync(UserEntity user);
        Task SendAppointmentConfirmationEmailAsync(UserEntity user, DoctorEntity doctor, AppointmentEntity appointment);
        Task SendForgotPasswordEmailAsync(string userEmail);
        Task SendWelcomeEmailAsync(string firstName, string lastName);
        Task SendAppointmentReminderAsync();
        Task SendAppointmentFeedbackEmailAsync();
    }
}
