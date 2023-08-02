using DammitBot.Library;

namespace DammitBot.Plugins;

public class SchedulingPlugin : ISchedulingPlugin
{
    #region Private Members

    private readonly ISchedulerService _schedulerService;

    #endregion

    #region Constructors

    public SchedulingPlugin(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }

    #endregion

    #region Exposed Methods

    public void Initialize()
    {
        _schedulerService.Start();
    }

    public void Cleanup()
    {
        _schedulerService.Stop();
    }

    #endregion
}