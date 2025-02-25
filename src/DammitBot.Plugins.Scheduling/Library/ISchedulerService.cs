﻿using System.Threading.Tasks;

namespace DammitBot.Library;

/// <summary>
/// Service responsible for starting and stopping all schedulable recurring jobs.
/// </summary>
public interface ISchedulerService
{
    #region Abstract Methods

    /// <summary>
    /// Build and schedule all available jobs so they start running at their intervals.
    /// </summary>
    Task Start();
    /// <summary>
    /// Unschedule all jobs and shut down.
    /// </summary>
    Task Stop();

    #endregion
}
