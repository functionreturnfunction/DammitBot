namespace DammitBot.Metadata;

/// <summary>
/// Specifies the units used in a time interval (hours, minutes, seconds, etc.)
/// </summary>
public enum IntervalType
{
    /// <summary>
    /// Interval measured in hours.
    /// </summary>
    Hourly,
    /// <summary>
    /// Interval measured in minutes.
    /// </summary>
    Minutely,
    /// <summary>
    /// Interval measured in seconds.
    /// </summary>
    Secondly
}