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
        #region Private Methods

        private static IScheduler CreateScheduler(IJobFactory jobFactory)
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler();
            scheduler.JobFactory = jobFactory;
            return scheduler;
        }

        #endregion

        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<IJobFactory>().Use<JobFactory>();
            e.For<ISchedulerService>().Use<SchedulerService>().Singleton();
            e.For<IScheduler>().Use(ctx => CreateScheduler(ctx.GetInstance<IJobFactory>()));
        }

        #endregion
    }
}
