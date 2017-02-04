using System;
using System.Collections.Generic;
using Quartz;

namespace DammitBot.Scheduling.Library
{
    public class SchedulerService : ISchedulerService
    {
        #region Private Members

        private readonly IScheduler _scheduler;
        private readonly IJobService _jobService;
        private readonly IList<TriggerKey> _triggerKeys;

        #endregion

        #region Constructors

        public SchedulerService(IScheduler scheduler, IJobService jobService)
        {
            _scheduler = scheduler;
            _jobService = jobService;
            _triggerKeys = new List<TriggerKey>();
        }

        #endregion

        #region Private Methods

        private void ScheduleJob(Type jobType)
        {
            var name = jobType.Name;
            var group = name.Replace("Job", "Group");
            var triggerName = name.Replace("Job", "Trigger");
            var job = _jobService.Build(jobType, name, group);
            var trigger = _jobService.BuildTrigger(jobType, triggerName, group);

            _scheduler.ScheduleJob(job, trigger);

            _triggerKeys.Add(trigger.Key);
        }

        #endregion

        #region Exposed Methods

        public void Start()
        {
            _scheduler.Start();

            foreach (var job in _jobService.GetAllJobs())
            {
                ScheduleJob(job);
            }
        }

        public void Stop()
        {
            // TODO: figure out why this needs to happen
            if (!_scheduler.IsShutdown)
            {
                _scheduler.UnscheduleJobs(_triggerKeys);
                _triggerKeys.Clear();
                _scheduler.Shutdown();
            }
        }

        #endregion
    }
}
