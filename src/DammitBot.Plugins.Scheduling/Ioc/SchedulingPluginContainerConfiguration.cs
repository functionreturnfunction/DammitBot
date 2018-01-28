using System.Threading.Tasks;
using DammitBot.Abstract;
using DammitBot.Scheduling.Library;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using StructureMap;

namespace DammitBot.Scheduling.Ioc
{
    public class SchedulingPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<IJobFactory>().Use<JobFactory>();
            e.For<ISchedulerService>().Use<SchedulerService>().Singleton();
        }

        #endregion
    }
}
