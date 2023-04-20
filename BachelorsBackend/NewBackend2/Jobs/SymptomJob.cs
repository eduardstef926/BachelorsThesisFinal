
using NewBackend2.Service.Abstract;
using Quartz;

public class SymptomJob : IJob
{
    private ICoreService coreService;

    public SymptomJob(ICoreService coreService)
    {
        this.coreService = coreService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await coreService.UpdateSymptomsAsync();
    }
}