using System;
using System.Text.RegularExpressions;
using DammitBot.Data.Models;

namespace DammitBot.Utilities
{
    public class ReminderTextGenerator : IReminderTextGenerator
    {
        #region Private Methods

        private string FromStrings(string to, string from, string text)
        {
            return $"@{to} {from} wanted me to remind you {FixNouns(text)}";
        }

        private Reminder DifferentUser(Reminder reminder)
        {
            reminder.Text = FromStrings(reminder.To.Username, reminder.From.Username, FixNouns(reminder.Text));
            return reminder;
        }

        private string FixNouns(string reminder)
        {
            return reminder;
        }

        private Reminder SameUser(Reminder reminder)
        {
            reminder.Text = FromStrings(reminder.To.Username, "you", FixNouns(reminder.Text));
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
