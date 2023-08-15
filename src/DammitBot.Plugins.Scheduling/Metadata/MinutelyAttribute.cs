using DammitBot.Library;

namespace DammitBot.Metadata;

/// <summary>
/// Used to specify that a job should run once per minute, or once every x number of minutes.
/// </summary>
public class MinutelyAttribute : IntervalAttribute
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MinutelyAttribute"/> class, specifies that a job should run once
    /// per minute. 
    /// </summary>
    public MinutelyAttribute() : base(1, IntervalType.Minutely) {}
    /// <summary>
    /// Constructor for the <see cref="MinutelyAttribute"/> class, specifies that a job should run once
    /// every <paramref name="minutes"/> minutes.
    /// </summary>
    public MinutelyAttribute(int minutes) : base(minutes, IntervalType.Minutely) {}

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public override ISimpleScheduleBuilder SetInterval(ISimpleScheduleBuilder builder)
    {
        return builder.WithIntervalInMinutes(Interval);
    }

    #endregion
}