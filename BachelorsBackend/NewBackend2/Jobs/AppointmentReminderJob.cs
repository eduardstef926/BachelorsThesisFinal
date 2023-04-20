using NewBackend2.Service.Abstract;
using Quartz;

namespace NewBackend2.Jobs
{
    public class AppointmentReminderJob : IJob
    {
        private IEmailService emailService;

        public AppointmentReminderJob(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await emailService.SendAppointmentReminderAsync();
        }
    }
}
