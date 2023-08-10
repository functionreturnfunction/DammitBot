using System.Threading.Tasks;

﻿namespace DammitBot.Library;

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
