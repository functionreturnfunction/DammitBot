using System;
using DammitBot.Data.Models;

namespace DammitBot.Utilities
{
    public class ReminderTextGenerator : IReminderTextGenerator
    {
        #region Private Methods

        private string FromStrings(string to, string from, string text, DateTime when)
        {
            return $"@{to} {from} wanted me to remind you {FixNouns(text)} on {when:d} at {when:t}";
        }

        private Reminder DifferentUser(Reminder reminder)
        {
            reminder.Text = FromStrings(
                reminder.To.Username,
                reminder.From.Username,
                FixNouns(reminder.Text),
                reminder.RemindAt.Value);
            return reminder;
        }

        private string FixNouns(string reminder)
        {
            return reminder;
        }

        private Reminder SameUser(Reminder reminder)
        {
            reminder.Text = FromStrings(
                reminder.To.Username,
                "you",
                FixNouns(reminder.Text), reminder.RemindAt.Value);
            return reminder;
        }

        #endregion

        #region Exposed Methods

        public Reminder Generate(Reminder reminder)
        {
            return reminder.From == reminder.To ? SameUser(reminder) : DifferentUser(reminder);
        }

        #endregion
    }
}
