using System;

namespace DateTimeStringParser;

public class DateTimeProvider : IDateTimeProvider
{
    #region Exposed Methods

    public virtual DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    public virtual DateTime GetNext(int hour, int minute = 0, int second = 0)
    {
        return GetCurrentTime().GetNext(hour, minute, second);
    }

    #endregion
}