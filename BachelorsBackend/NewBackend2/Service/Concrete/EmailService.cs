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
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendWelcomeEmailAsync(string firstName, string lastName)
        {
            var subject = "Thank you for sigining in";
            var user = await userRepository.GetUserByFirstNameAndLastNameAsync(firstName, lastName);
            var body = EmailHelper.GetUserWelcomeEmailTemplate();
            body = body.Replace("[Recipient Name]", user.LastName);

            var email = new EmailEntity
            {
                To = user.Email,
                Message = body,
                Subject = subject,
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendAppointmentConfirmationEmailAsync(UserEntity user, DoctorEntity doctor, AppointmentEntity appointment)
        {
            var subject = "Appointment Confirmed";
            var body = EmailHelper.GetAppointmentConfirmationEmailTemplate();
            var appointmentDate = appointment.AppointmentDate.Day.ToString() + "/" + appointment.AppointmentDate.Month.ToString() + "/" + appointment.AppointmentDate.Year.ToString();
            var appointmentHour = appointment.AppointmentDate.Hour.ToString() + ":" + appointment.AppointmentDate.Minute.ToString() + "0";

            body = body.Replace("[Recipient Name]", user.FirstName)
                       .Replace("[Doctor Name]", doctor.FirstName + " " + doctor.LastName)
                       .Replace("[Hospital]", appointment.HospitalName)
                       .Replace("[City]", appointment.Location)
                       .Replace("[Date]", appointmentDate)
                       .Replace("[Hour]", appointmentHour)
                       .Replace("[Cost]", appointment.Price.ToString());

            var email = new EmailEntity
            {
                To = user.Email,
                Message = body,
                Subject = subject,
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendAppointmentReminderAsync()
        {
            var currentDate = DateTime.Now;
            var appointments = await appointmentRepository.GetFullAppointmentsDataAsync();

            foreach(var appointment in appointments)
            {
                var difference = appointment.AppointmentDate - currentDate;
                if (difference.Days <= 1)
                {
                    var subject = "Appointment Reminder";
                    var body = EmailHelper.GetAppointmentReminderEmailTemplate();
                    var appointmentDate = appointment.AppointmentDate.Day.ToString() + "/" + appointment.AppointmentDate.Month.ToString() + "/" + appointment.AppointmentDate.Year.ToString();
                    var appointmentHour = appointment.AppointmentDate.Hour.ToString() + ":" + appointment.AppointmentDate.Minute.ToString() + "0";

                    body = body.Replace("[Recipient Name]", appointment.User.FirstName)
                               .Replace("[Doctor Name]", appointment.Doctor.FirstName + " " + appointment.Doctor.LastName)
                               .Replace("[Hospital]", appointment.HospitalName)
                               .Replace("[City]", appointment.Location)
                               .Replace("[Date]", appointmentDate)
                               .Replace("[Hour]", appointmentHour)
                               .Replace("[Cost]", appointment.Price.ToString());

                    var email = new EmailEntity
                    {
                        To = appointment.User.Email,
                        Message = body,
                        Subject = subject,
                    };

                    await this.SendEmailAsync(email);
                    await emailRepository.AddEmailAsync(email);
                }
            }
        }

        public async Task SendAppointmentFeedbackEmailAsync()
        {
            var currentDate = DateTime.Now;
            var appointments = await appointmentRepository.GetFullAppointmentsDataAsync();

            foreach (var appointment in appointments)
            {
                var difference = appointment.AppointmentDate - currentDate;
                if (difference.Days >=-1 && difference.Days <= 0)
                {
                    var subject = "Appointment Feedback";
                    var body = EmailHelper.GetReviewEmailTemplate();
                    var userLink = "http://localhost:4200/appointment/review/" + appointment.AppointmentId;
                    body = body.Replace("[Recipient Name]", appointment.User.LastName)
                               .Replace("[Link]", userLink);

                    var email = new EmailEntity
                    {
                        To = appointment.User.Email,
                        Message = body,
                        Subject = subject,
                    };

                    await this.SendEmailAsync(email);
                    await emailRepository.AddEmailAsync(email);
                }
            }
        }

        public async Task SendSubscriptionPaymentAsync(UserEntity user, DateTime endDate)
        {
            var subject = "Subscription Confirmed";
            var body = EmailHelper.GetSubscriptionEmailPaymentTemplate();
            body = body.Replace("[Recipient Name]", user.FirstName)
                   .Replace("[End Date]", endDate.Year + "/" + endDate.Month + "/" + endDate.Day);

            var email = new EmailEntity
            {
                To = user.Email,
                Message = body,
                Subject = subject,
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }

        public async Task SendEmailConfirmationAsync(string userEmail)
        {
            var random = new Random();
            var confirmationCode = random.Next(1000, 10000);
            await userRepository.UpdateConfirmationCode(userEmail, confirmationCode);

            var user = await userRepository.GetUserByEmailAsync(userEmail);
            var subject = "Email Confirmation";
            var body = EmailHelper.GetConfirmationEmailTemplate();
            body = body.Replace("[Recipient Name]", user.LastName)
                       .Replace("[Confirmation Code]", confirmationCode.ToString());

            var email = new EmailEntity
            {
                To = user.Email,
                Message = body,
                Subject = subject,
            };

            await this.SendEmailAsync(email);
            await emailRepository.AddEmailAsync(email);
        }
    }
}
