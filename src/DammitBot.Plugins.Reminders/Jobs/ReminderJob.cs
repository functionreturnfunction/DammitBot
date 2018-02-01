using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILog _log;

        #endregion

        #region Constructors

        public ReminderJob(IBot bot, IUnitOfWorkFactory unitOfWorkFactory, IDateTimeProvider dateTimeProvider, ILog log)
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
            using (var uow = _unitOfWorkFactory.Build().Start())
            {
                var now = _dateTimeProvider.GetCurrentTime();
                var reminders = GetReminders(now);
                _log.Debug($"Found {reminders.Count()} reminders due as of {now}");
                foreach (var reminder in reminders)
                {
                    _bot.SayToAll(reminder.Text);
                    reminder.RemindedAt = _dateTimeProvider.GetCurrentTime();
                    uow.Insert(reminder);
                }

                uow.Commit();
            }
        }

        // Task IJob.Execute(IJobExecutionContext context)
        // {
        //     throw new NotImplementedException();
        // }

        private IQueryable<Reminder> GetReminders(DateTime since)
        {
            using (var uow = _unitOfWorkFactory.Build().Start())
            {
                return uow.Query<Reminder>().Where(r => !r.RemindedAt.HasValue && r.RemindAt <= since);
            }
        }

        #endregion
    }
}
