using System;

namespace DateTimeStringParser;

public static class DateTimeExtensions
{
    /// <summary>
    /// Get the next instance of an hour (+ minute and second).
    /// 
    /// For instance, if it's currently 7 am, calling this for 8 will return 
    /// the current date at 8 am.  If it's currently 8:30 am, calling this
    /// for 8 will return the next day at 8 am.
    /// </summary>
    /// <param name="hour">Hour in 24 hour time.</param>
    public static DateTime GetNext(this DateTime date, int hour, int minute = 0, int second = 0)
    {
        if ((date.Hour > hour || (date.Hour == hour && date.Minute >= minute)))
        {
            date = date.AddDays(1);
        }

        return new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
    }
}