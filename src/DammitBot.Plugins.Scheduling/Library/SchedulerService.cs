﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Quartz;
using Quartz.Spi;

namespace DammitBot.Library;

/// <inheritdoc />
public class SchedulerService : ISchedulerService
{
    #region Private Members

    private IScheduler? _scheduler;
    private readonly IStdSchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IJobService _jobService;
    private readonly IList<TriggerKey> _triggerKeys;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SchedulerService"/> job.
    /// </summary>
    public SchedulerService(
        IStdSchedulerFactory schedulerFactory,
        IJobFactory jobFactory,
        IJobService jobService)
    {
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
        _jobService = jobService;
        _triggerKeys = new List<TriggerKey>();
    }

    #endregion

    #region Private Methods

    private async Task ScheduleJob(Type jobType)
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

    /// <inheritdoc />
    public async Task Start()
    {
        _schedulerFactory.Initialize();
        _scheduler = await _schedulerFactory.GetScheduler();
        _scheduler.JobFactory = _jobFactory;

        await _scheduler.Start();

        foreach (var job in _jobService.GetAllJobs())
        {
            await ScheduleJob(job);
        }
    }

    /// <inheritdoc />
    public async Task Stop()
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
