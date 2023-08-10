using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

/// <inheritdoc />
public interface IReminderRepository : IRepository<Reminder>
{
    #region Abstract Methods

    /// <summary>
    /// Get all reminders which come due at or before <see cref="DateTime"/>
    /// <paramref name="since"/> and haven't been sent yet.
    /// </summary>
    Task<IEnumerable<Reminder>> GetPendingAsync(DateTime since);
    

    #endregion
}