using System;

namespace DammitBot.Utilities
{
    public class DateTimeProvider : IDateTimeProvider
    {
        #region Exposed Methods

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        #endregion
    }
}
