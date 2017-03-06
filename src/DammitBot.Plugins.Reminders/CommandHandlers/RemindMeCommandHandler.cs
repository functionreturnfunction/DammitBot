using System;
using System.Text.RegularExpressions;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Metadata;
using DateTimeStringParser;

namespace DammitBot.CommandHandlers
{
    [HandlesCommand("^remind me.*")]
    public class RemindMeCommandHandler : CommandHandlerBase
    {
        #region Constants

        public const string REGEX = "^remind me (?:to|that) (.+) ((?:at|in) (.+)|tomorrow(?: morning)?)$";

        #endregion

        #region Private Members

        private readonly IPersistenceService _persistenceService;
        private readonly IDateTimeStringParser _dateTimeStringParser;

        #endregion

        #region Constructors

        public RemindMeCommandHandler(IBot bot, IPersistenceService persistenceService, IDateTimeStringParser dateTimeStringParser) : base(bot)
        {
            _persistenceService = persistenceService;
            _dateTimeStringParser = dateTimeStringParser;
        }

        #endregion

        #region Private Methods

        private Reminder CreateReminder(string reminder, User from, User to, DateTime when)
        {
            using (_persistenceService)
            {
                return _persistenceService.Save(new Reminder {
                    Text = reminder,
                    From = from,
                    To = to,
                    RemindAt = when
                });
            }
        }

        #endregion

        #region Exposed Methods

        public override void Handle(CommandEventArgs e)
        {
            DateTime? when;
            var match = new Regex(REGEX).Match(e.Command);
            var reminder = match.Groups[1].Value;
            var timeStr = match.Groups[2].Value;
            if (_dateTimeStringParser.TryParse(timeStr, out when))
            {
                var entity = CreateReminder(reminder, e.From.User, e.From.User, when.Value);
                _bot.ReplyToMessage(e, $"Reminder set for {when}");
            }
            else
            {
                _bot.ReplyToMessage(e, $"Cannot parse time string '{timeStr}'");
            }

        }

        #endregion
    }
}
