using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace DammitBot.Library;

/// <inheritdoc />
public class SchedulerService : ISchedulerService
{
    #region Private Members

    private IScheduler? _scheduler;
    private readonly IJobFactory _jobFactory;
    private readonly IJobService _jobService;
    private readonly IList<TriggerKey> _triggerKeys;

    #endregion

    #region Constructors

    public SchedulerService(IJobFactory jobFactory, IJobService jobService)
    {
        _jobFactory = jobFactory;
        _jobService = jobService;
        _triggerKeys = new List<TriggerKey>();
    }

    #endregion

    #region Private Methods

    private async void ScheduleJob(Type jobType)
    {
        var name = jobType.Name;
        var group = name.Replace("Job", "Group");
        var triggerName = name.Replace("Job", "Trigger");
        var job = _jobService.Build(jobType, name, group);
        var trigger = _jobService.BuildTrigger(jobType, triggerName, group);

        // this gets called by Start() which sets _scheduler, so in theory this will never be null
        // here
        await _scheduler!.ScheduleJob(job, trigger);

        _triggerKeys.Add(trigger.Key);
    }

    #endregion

    #region Exposed Methods

    public async void Start()
    /// <inheritdoc />
    {
        var factory = new StdSchedulerFactory();
        factory.Initialize();
        _scheduler = await factory.GetScheduler();
        _scheduler.JobFactory = _jobFactory;

        await _scheduler.Start();

        foreach (var job in _jobService.GetAllJobs())
        {
            ScheduleJob(job);
        }
    }

    public async void Stop()
    /// <inheritdoc />
    {
        if (_scheduler == null)
        {
            throw new InvalidOperationException(
                "Cannot stop service before it has been started");
        }
            
        // TODO: figure out why this needs to happen
        if (!_scheduler.IsShutdown)
        {
            await _scheduler.UnscheduleJobs(
                new ReadOnlyCollection<TriggerKey>(_triggerKeys));
            _triggerKeys.Clear();
            await _scheduler.Shutdown();
        }
    }

    #endregion
}