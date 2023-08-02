using DammitBot.Abstract;
using DammitBot.Library;
using Quartz.Spi;
using StructureMap;

namespace DammitBot.Ioc;

public class SchedulingPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods

    public override void Configure(ConfigurationExpression e)
    {
        e.For<IJobFactory>().Use<JobFactory>();
        e.For<ISchedulerService>().Use<SchedulerService>().Singleton();
    }

    #endregion
}