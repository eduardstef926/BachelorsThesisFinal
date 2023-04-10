
using NewBackend2.Service.Abstract;
using Quartz;

public class SymptomJob : IJob
{
    private ICoreService _coreService;

    public SymptomJob(ICoreService coreService)
    {
        _coreService = coreService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _coreService.UpdateSymptomsAsync();
    }
}