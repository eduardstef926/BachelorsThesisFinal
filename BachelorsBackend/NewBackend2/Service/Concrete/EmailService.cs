using NewBackend2.Helpers;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;
using System.Net;
using System.Net.Mail;

namespace NewBackend2.Service.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository emailRepository;
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public EmailService(IUserRepository userRepository, IEmailRepository emailRepository, IAppointmentRepository appointmentRepository)
        {
            this.emailRepository = emailRepository; 
            this.userRepository = userRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public async Task SendEmailAsync(EmailEntity email)
        {
            var fromEmail = EmailHelper.GetUserEmail();
            var fromPassword = EmailHelper.GetUserPassword();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromEmail);
            message.Subject = email.Subject;
            message.To.Add(new MailAddress(email.To));
            message.Body = email.Message;
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public async Task SendForgotPasswordEmailAsync(string userEmail)
        {
            var subject = "Password recovery";
            var user = await userRepository.GetUserByEmailAsync(userEmail);
            var body = EmailHelper.GetPasswordResetEmailTemplate();
            var userLink = "http://localhost:4200/modify-password/" + user.UserId;
            body = body.Replace("[Recipient Name]", user.LastName)
                       .Replace("[Link]", userLink);

            var email = new EmailEntity
            {
                To = userEmail,
                Message = body,
                Subject = subject,
                Send = false
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendWelcomeEmailAsync(string firstName, string lastName)
        {
            var subject = "Thank you for sigining in";
            var user = await userRepository.GetUserByFirstNameAndLastNameAsync(firstName, lastName);
            var body = EmailHelper.GetUserWelcomeEmailTemplate();
            var userLink = "http://localhost:4200/main/" + user.UserId;
            body = body.Replace("[Recipient Name]", user.LastName)
                       .Replace("[Link]", userLink);

            var email = new EmailEntity
            {
                To = user.Email,
                Message = body,
                Subject = subject,
                Send = false
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendAppointmentConfirmationEmailAsync(DateTime appointmentDate)
        {
            var subject = "Appointment Confirmed";
            var appointment = await appointmentRepository.GetAppointmentByDateAsync(appointmentDate);
            var body = EmailHelper.GetAppointmentConfirmationEmailTemplate();
            body = body.Replace("[Recipient Name]", appointment.User.FirstName)
                       .Replace("[StartDate]", appointment.AppointmentDate.Date.ToString())
                       .Replace("[Location]", appointment.Location);

            var email = new EmailEntity
            {
                To = appointment.User.Email,
                Message = body,
                Subject = subject,
                Send = false
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }
    }
}
