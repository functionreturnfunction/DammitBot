using System;
using System.Linq;
using System.Text.RegularExpressions;
using DammitBot.Data.Models;
using DammitBot.Events;
using DammitBot.Library;
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

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IDateTimeStringParser _dateTimeStringParser;
        private readonly IReminderTextGenerator _reminderTextGenerator;

        #endregion

        #region Constructors

        public ReminderCommandHandler(IBot bot, IUnitOfWorkFactory unitOfWorkFactory, IDateTimeStringParser dateTimeStringParser, IReminderTextGenerator reminderTextGenerator) : base(bot)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _dateTimeStringParser = dateTimeStringParser;
            _reminderTextGenerator = reminderTextGenerator;
        }

        #endregion

        #region Private Methods

        private Reminder CreateReminder(string reminder, User from, User to, DateTime when, IUnitOfWork uow)
        {
            var obj = _reminderTextGenerator.Generate(new Reminder
            {
                Text = reminder,
                From = from,
                To = to,
                RemindAt = when
            });

            obj.Id = Convert.ToInt32(uow.Insert(obj));

            return obj;
        }

        #endregion

        #region Exposed Methods

        public override void Handle(CommandEventArgs e)
        {
            DateTime? when;
            var match = new Regex(REGEX).Match(e.Command);
            User target;
            var targetStr = match.Groups[1].Value;
            var reminder = match.Groups[2].Value;
            var timeStr = match.Groups[3].Value;

            if (!_dateTimeStringParser.TryParse(timeStr, out when))
            {
                _bot.ReplyToMessage(e, $"Cannot parse time string '{timeStr}'");
                return;
            }

            using (var uow = _unitOfWorkFactory.Build())
            {
                target = LoadTarget(e, uow, targetStr);

                if (target == null)
                {
                    _bot.ReplyToMessage(e, $"Cannot find user with username '{targetStr}'");
                    return;
                }

                CreateReminder(reminder, e.From.User, target, when.Value, uow);
            }

            _bot.ReplyToMessage(e, $"Reminder set for {when}");
        }

        private User LoadTarget(CommandEventArgs commandEventArgs, IUnitOfWork uow, string value)
        {
            if (value == "me")
            {
                return commandEventArgs.From.User;
            }

            return value == "me" ? commandEventArgs.From.User : uow.Query<User>().SingleOrDefault(u => u.Username == value);
        }

        #endregion
    }
}
