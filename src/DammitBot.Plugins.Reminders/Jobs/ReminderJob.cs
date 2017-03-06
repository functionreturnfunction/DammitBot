using System;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Scheduling.Metadata;
using DateTimeStringParser;
using log4net;
using Quartz;

namespace DammitBot.Jobs
{
    [Secondly(15)]
    public class ReminderJob : IJob
    {
        #region Private Members

        private readonly IBot _bot;
        private readonly IPersistenceService _persistenceService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILog _log;

        #endregion

        #region Constructors

        public ReminderJob(IBot bot, IPersistenceService persistenceService, IDateTimeProvider dateTimeProvider, ILog log)
        {
            _bot = bot;
            _persistenceService = persistenceService;
            _dateTimeProvider = dateTimeProvider;
            _log = log;
        }

        #endregion

        #region Exposed Methods

        public void Execute(IJobExecutionContext context)
        {
            using (_persistenceService)
            {
                var now = _dateTimeProvider.GetCurrentTime();
                var reminders = GetReminders(now);
                _log.Debug($"Found {reminders.Count()} reminders due as of {now}");
                foreach (var reminder in reminders)
                {
                    _bot.SayToAll(reminder.Text);
                    reminder.RemindedAt = _dateTimeProvider.GetCurrentTime();
                    _persistenceService.Save(reminder);
                }
            }
        }

        private IQueryable<Reminder> GetReminders(DateTime since)
        {
            return _persistenceService.Query<Reminder>().Where(r => !r.RemindedAt.HasValue && r.RemindAt <= since);
        }

        #endregion
    }
}
