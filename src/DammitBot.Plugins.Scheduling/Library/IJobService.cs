using System;
using System.Collections.Generic;
using DammitBot.Metadata;
using Quartz;

namespace DammitBot.Library;

/// <summary>
/// Service responsible for locating <see cref="Type"/>s of available jobs, building them into
/// <see cref="IJobDetail"/>s, and building <see cref="ITrigger"/>s for them.
/// </summary>
public interface IJobService
{
    #region Abstract Methods
    
    /// <summary>
    /// Get all available <see cref="Type"/>s which implement <see cref="IJob"/>, are concrete, and have
    /// a class name ending with "Job".
    /// </summary>
    IEnumerable<Type> GetAllJobs();
    /// <summary>
    /// Build an <see cref="IJobDetail"/> for the specified <paramref name="jobType"/> with the specified
    /// <paramref name="name"/> within the specified <paramref name="group"/>.
    /// </summary>
    IJobDetail Build(Type jobType, string name, string group);
    /// <summary>
    /// Build an <see cref="ITrigger"/> for the specified <see cref="jobType"/> based on its
    /// <see cref="IntervalAttribute"/> (or lack thereof) with the specified
    /// <paramref name="triggerName"/> within the specified <paramref name="group"/>. 
    /// </summary>
    ITrigger BuildTrigger(Type jobType, string triggerName, string group);
    
    #endregion
}