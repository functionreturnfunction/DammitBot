using DammitBot.Library;

namespace DammitBot.Metadata;

/// <summary>
/// Used to specify that a job should run hourly, or every x hours.
/// </summary>
public class HourlyAttribute : IntervalAttribute
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="HourlyAttribute"/> class, specifies that a job should run once per
    /// hour.
    /// </summary>
    public HourlyAttribute() : base(1, IntervalType.Hourly) {}
    /// <summary>
    /// Constructor for the <see cref="HourlyAttribute"/> class, specifies that a job should run once
    /// every <paramref name="hours"/> hours.
    /// </summary>
    /// <param name="hours"></param>
    public HourlyAttribute(int hours) : base(hours, IntervalType.Hourly) {}

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public override ISimpleScheduleBuilder SetInterval(ISimpleScheduleBuilder builder)
    {
        return builder.WithIntervalInHours(Interval);
    }

    #endregion
}