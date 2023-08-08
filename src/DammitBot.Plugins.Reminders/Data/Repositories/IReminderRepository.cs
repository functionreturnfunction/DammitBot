using System;
using System.Collections.Generic;
using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

public interface IReminderRepository : IRepository<Reminder>
{
    IEnumerable<Reminder> GetPending(DateTime since);
}