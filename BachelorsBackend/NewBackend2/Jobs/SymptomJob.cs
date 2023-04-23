
using NewBackend2.Service.Abstract;
using Quartz;

public class SymptomJob : IJob
{
    private IUserService userService;

    public SymptomJob(IUserService coreService)
    {
        this.userService = coreService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await userService.UpdateSymptomsAsync();
    }
}