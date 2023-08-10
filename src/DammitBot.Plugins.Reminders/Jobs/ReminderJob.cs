using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using DammitBot.Metadata;
using DateTimeProvider;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DammitBot.Jobs;

/// <summary>
/// <see cref="IJob"/> implementation which when run will send any pending reminders and record having
/// sent them.
/// </summary>
[Secondly(15)]
public class ReminderJob : IJob
{
    #region Private Members

    private readonly IBot _bot;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger _log;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="ReminderJob"/> class.
    /// </summary>
    public ReminderJob(
        IBot bot,
        IUnitOfWorkFactory unitOfWorkFactory,
        IDateTimeProvider dateTimeProvider,
        ILogger<ReminderJob> log)
    {
        _bot = bot;
        _unitOfWorkFactory = unitOfWorkFactory;
        _dateTimeProvider = dateTimeProvider;
        _log = log;
    }

    #endregion

    #region Exposed Methods

    /// <summary>
    /// Find all pending reminders, send their messages, and mark them 
    /// </summary>
    public async Task Execute(IJobExecutionContext context)
    {
        using var uow = _unitOfWorkFactory.Build();
        var now = _dateTimeProvider.GetCurrentTime();
        var reminders = await GetReminders(uow, now);
        _log.LogDebug($"Found {reminders.Count()} reminders due as of {now}");
        foreach (var reminder in reminders)
        {
            _bot.SayToAll(reminder.Text);
            reminder.RemindedAt = _dateTimeProvider.GetCurrentTime();
            await uow.UpdateAsync(reminder);
        }

        uow.Commit();
    }

    private async Task<IEnumerable<Reminder>> GetReminders(IUnitOfWork uow, DateTime since)
    {
        return await uow.GetRepository<IReminderRepository, Reminder>().GetPendingAsync(since);
    }

    #endregion
}
