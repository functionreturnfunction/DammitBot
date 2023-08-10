using DammitBot.Data.Models;

namespace DammitBot.Utilities;

/// <summary>
/// Generator of proper reminder text, based on who sent the reminder and to whom.
/// </summary>
public interface IReminderTextGenerator
{
    #region Abstract Methods

    /// <summary>
    /// Update <paramref name="reminder"/>'s <see cref="Reminder.Text"/> property to a properly-reworded
    /// message based on the existing value and who sent the reminder to whom.
    /// </summary>
    Reminder Generate(Reminder reminder);

    #endregion
}