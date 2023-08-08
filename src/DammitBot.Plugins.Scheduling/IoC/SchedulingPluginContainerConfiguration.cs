using DammitBot.Abstract;
using DammitBot.Library;
using Lamar;
using Quartz.Spi;

namespace DammitBot.IoC;

public class SchedulingPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    public override void Configure(ServiceRegistry e)
    {
        e.For<IJobFactory>().Use<JobFactory>();
        e.For<ISchedulerService>().Use<SchedulerService>().Singleton();
    }

    #endregion
}