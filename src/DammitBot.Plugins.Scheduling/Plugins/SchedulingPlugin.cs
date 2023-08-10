using DammitBot.Library;

namespace DammitBot.Plugins;

/// <inheritdoc />
public class SchedulingPlugin : ISchedulingPlugin
{
    #region Private Members

    private readonly ISchedulerService _schedulerService;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SchedulingPlugin"/> class.
    /// </summary>
    public SchedulingPlugin(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    /// <remarks>
    /// This implementation starts a <see cref="ISchedulerService"/>.
    /// </remarks>
    public void Initialize()
    {
        _schedulerService.Start();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation stops a <see cref="ISchedulerService"/>.
    /// </remarks>
    public void Cleanup()
    {
        _schedulerService.Stop();
    }

    #endregion
}