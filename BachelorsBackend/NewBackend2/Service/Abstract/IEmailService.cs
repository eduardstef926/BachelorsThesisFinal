using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailEntity email);
        public Task SendForgotPasswordEmailAsync(string userEmail);
        public Task SendWelcomeEmailAsync(string firstName, string lastName);
    }
}
