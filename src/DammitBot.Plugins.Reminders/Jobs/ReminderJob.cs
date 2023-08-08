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

    public async Task Execute(IJobExecutionContext context)
    {
        using var uow = _unitOfWorkFactory.Build();
        var now = _dateTimeProvider.GetCurrentTime();
        var reminders = GetReminders(uow, now);
        _log.LogDebug($"Found {reminders.Count()} reminders due as of {now}");
        foreach (var reminder in reminders)
        {
            _bot.SayToAll(reminder.Text);
            reminder.RemindedAt = _dateTimeProvider.GetCurrentTime();
            uow.Insert(reminder);
        }

        uow.Commit();
    }

    // Task IJob.Execute(IJobExecutionContext context)
    // {
    //     throw new NotImplementedException();
    // }

    private IEnumerable<Reminder> GetReminders(IUnitOfWork uow, DateTime since)
    {
        return uow.GetRepository<IReminderRepository, Reminder>().GetPending(since);
    }

    #endregion
}