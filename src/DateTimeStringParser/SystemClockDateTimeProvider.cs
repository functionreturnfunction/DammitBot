using System;

namespace DateTimeStringParser;


/// <inheritdoc cref="IDateTimeProvider" />
/// <remarks>This implementation uses the system clock via <see cref="DateTime.Now"/>.</remarks>
public class SystemClockDateTimeProvider : IDateTimeProvider
{
    #region Exposed Methods

    /// <inheritdoc cref="IDateTimeProvider.GetCurrentTime"/>
    public virtual DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    /// <inheritdoc cref="IDateTimeProvider.GetNext"/>
    public virtual DateTime GetNext(int hour, int minute = 0, int second = 0)
    {
        return GetCurrentTime().GetNext(hour, minute, second);
    }

    #endregion
}