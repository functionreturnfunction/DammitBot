using DammitBot.Data.Models;

namespace DammitBot.Utilities
{
    public interface IReminderTextGenerator
    {
        #region Abstract Methods

        Reminder Generate(Reminder reminder);

        #endregion
    }
}