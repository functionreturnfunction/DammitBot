using Quartz;

namespace DammitBot.Metadata;

/// <summary>
/// Used to specify that a job should run daily, or ever x number of days.
/// </summary>
public class DailyAttribute : IntervalAttribute
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DailyAttribute"/>, specifies that a job should run once per day.
    /// </summary>
    public DailyAttribute() : base(24, IntervalType.Hourly) {}
    /// <summary>
    /// Constructor for the <see cref="DailyAttribute"/>, specifies that a job should run once every
    /// <paramref name="days"/> days.
    /// </summary>
    public DailyAttribute(int days) : base(24 * days, IntervalType.Hourly) {}

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public override SimpleScheduleBuilder SetInterval(SimpleScheduleBuilder builder)
    {
        return builder.WithIntervalInHours(Interval);
    }

    #endregion
}