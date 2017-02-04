using System;
using Quartz;
using Quartz.Spi;
using StructureMap;

namespace DammitBot.Scheduling.Library
{
    public class JobFactory : IJobFactory
    {
        #region Private Members

        private readonly IContainer _container;

        #endregion

        #region Constructors

        public JobFactory(IContainer container)
        {
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.GetInstance(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job) {}

        #endregion
    }
}
