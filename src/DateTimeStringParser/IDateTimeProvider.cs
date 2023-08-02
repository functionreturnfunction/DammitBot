using System;

namespace DateTimeStringParser;

public interface IDateTimeProvider
{
    DateTime GetCurrentTime();
    DateTime GetNext(int hour, int minute = 0, int second = 0);
}