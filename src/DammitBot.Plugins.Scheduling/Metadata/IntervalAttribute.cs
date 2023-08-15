using System;
using DammitBot.Library;
using Quartz;

namespace DammitBot.Metadata;

/// <summary>
/// <see cref="Attribute"/> used to determine the interval at which a job should be repeated.  Jobs
/// without this attribute will default to every 24 hours.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public abstract class IntervalAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Number of time units (see: <see cref="IntervalType"/>) between each job run.
    /// </summary>
    public int Interval { get; private set; }
    /// <summary>
    /// Time units (hours, minutes, seconds, etc.) in use by the interval.
    /// </summary>
    public IntervalType IntervalType { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IntervalAttribute"/> class.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="intervalType"></param>
    protected IntervalAttribute(int interval, IntervalType intervalType)
    {
        Interval = interval;
        IntervalType = intervalType;
    }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Set the interval for the schedule based on the values in this attribute.
    /// </summary>
    public abstract ISimpleScheduleBuilder SetInterval(ISimpleScheduleBuilder builder);

    #endregion
}