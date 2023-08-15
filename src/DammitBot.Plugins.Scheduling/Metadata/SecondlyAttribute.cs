using DammitBot.Library;

namespace DammitBot.Metadata;

/// <summary>
/// Used to specify that a job should run once per second, or once every x number of seconds.
/// </summary>
public class SecondlyAttribute : IntervalAttribute
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SecondlyAttribute"/> class, specifies that a job should run once
    /// per second.
    /// </summary>
    public SecondlyAttribute() : base(1, IntervalType.Secondly) {}
    /// <summary>
    /// Constructor for the <see cref="SecondlyAttribute"/> class, specifies that a job should run once
    /// every <paramref name="seconds"/> seconds.
    /// </summary>
    public SecondlyAttribute(int seconds) : base(seconds, IntervalType.Secondly) {}

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public override ISimpleScheduleBuilder SetInterval(ISimpleScheduleBuilder builder)
    {
        return builder.WithIntervalInSeconds(Interval);
    }

    #endregion
}