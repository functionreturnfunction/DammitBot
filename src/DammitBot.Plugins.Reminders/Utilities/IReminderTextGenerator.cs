using System;
using DammitBot.Data.Models;

namespace DammitBot.Utilities
{
    public interface IReminderTextGenerator
    {
        Reminder Generate(Reminder reminder);
    }
}