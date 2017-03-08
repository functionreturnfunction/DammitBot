using System;
using System.Linq;
using System.Text.RegularExpressions;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;
using DateTimeStringParser;

namespace DammitBot.CommandHandlers
{
    [HandlesCommand(@"^remind ([^\s]+).+")]
    public class ReminderCommandHandler : CommandHandlerBase
    {
        #region Constants

        public const string REGEX = @"^remind ([^\s]+) ((?:to|that) .+) ((?:at|in) (.+)|tomorrow(?: morning)?)$";

        #endregion

        #region Private Members

        private readonly IPersistenceService _persistenceService;
        private readonly IDateTimeStringParser _dateTimeStringParser;
        private readonly IReminderTextGenerator _reminderTextGenerator;

        #endregion

        #region Constructors

        public ReminderCommandHandler(IBot bot, IPersistenceService persistenceService, IDateTimeStringParser dateTimeStringParser, IReminderTextGenerator reminderTextGenerator) : base(bot)
        {
            _persistenceService = persistenceService;
            _dateTimeStringParser = dateTimeStringParser;
            _reminderTextGenerator = reminderTextGenerator;
        }

        #endregion

        #region Private Methods

        private Reminder CreateReminder(string reminder, User from, User to, DateTime when)
        {
            var obj = _reminderTextGenerator.Generate(new Reminder {
                Text = reminder,
                From = from,
                To = to,
                RemindAt = when
            });
            return _persistenceService.Save(obj);
        }

        #endregion

        #region Exposed Methods

        public override void Handle(CommandEventArgs e)
        {
            DateTime? when;
            var match = new Regex(REGEX).Match(e.Command);

            using (_persistenceService)
            {
                var targetStr = match.Groups[1].Value;
                var target = LoadTarget(e, targetStr);
                var reminder = match.Groups[2].Value;
                var timeStr = match.Groups[3].Value;

                if (target == null)
                {
                    _bot.ReplyToMessage(e, $"Cannot find user with username '{targetStr}'");
                    return;
                }
                if (!_dateTimeStringParser.TryParse(timeStr, out when))
                {
                    _bot.ReplyToMessage(e, $"Cannot parse time string '{timeStr}'");
                    return;
                }

                var entity = CreateReminder(reminder, e.From.User, target, when.Value);
                _bot.ReplyToMessage(e, $"Reminder set for {when}");
            }
        }

        private User LoadTarget(CommandEventArgs commandEventArgs, string value)
        {
            return value == "me"
                ? commandEventArgs.From.User
                : _persistenceService.Query<User>().SingleOrDefault(u => u.Username == value);
        }

        #endregion
    }
}
