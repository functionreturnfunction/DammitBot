using DammitBot.Wrappers;
using Lamar;
using Quartz;
using Quartz.Spi;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation allows for <see cref="IJob"/> instantiation using dependency injection.
/// </remarks>
public class JobFactory : IJobFactory
{
    #region Private Members

    private readonly IInstantiationService _container;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="JobFactory"/> class.
    /// </summary>
    public JobFactory(IInstantiationService container)
    {
        _container = container;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="JobFactory" path="remarks" />
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return (IJob)_container.GetInstance(bundle.JobDetail.JobType);
    }

    /// <inheritdoc />
    public void ReturnJob(IJob job) {}

    #endregion
}