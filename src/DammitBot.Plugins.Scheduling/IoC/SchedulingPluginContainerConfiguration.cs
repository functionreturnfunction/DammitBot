using DammitBot.Abstract;
using DammitBot.Library;
using DammitBot.Wrappers;
using Lamar;
using Quartz.Spi;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers types used in scheduling jobs to run at preset intervals.
/// commands.
/// </remarks>
public class SchedulingPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    /// <inheritdoc />
    /// <inheritdoc cref="SchedulingPluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IJobFactory>().Use<JobFactory>();
        e.For<ISchedulerService>().Use<SchedulerService>().Singleton();
        e.For<IStdSchedulerFactory>().Use<StdSchedulerFactoryWrapper>();
    }

    #endregion
}