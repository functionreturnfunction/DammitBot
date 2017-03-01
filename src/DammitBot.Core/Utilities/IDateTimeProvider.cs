using System;

namespace DammitBot.Utilities
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentTime();
    }
}