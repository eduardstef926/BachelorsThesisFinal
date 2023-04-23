using NewBackend2.Service.Abstract;
using Quartz;

namespace NewBackend2.Jobs
{
    public class AppointmentFeedbackJob : IJob
    {
        private IEmailService emailService;

        public AppointmentFeedbackJob(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await emailService.SendAppointmentFeedbackEmailAsync();
        }
    }
}
