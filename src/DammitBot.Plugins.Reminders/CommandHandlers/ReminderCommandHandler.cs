using System;
using System.Text.RegularExpressions;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Metadata;
using DammitBot.Utilities;
using DateTimeProvider;
using DateTimeStringParser;

namespace DammitBot.CommandHandlers;

/// <summary>
/// <see cref="ICommandHandler"/> implementation which allows users to set reminders, which are messages
/// that the bot will send to a user or channel at a predefined point in the future.
/// </summary>
[HandlesCommand(
    @"^remind ([^\s]+).+",
    "Set reminders; messages which the bot will send to a user or channel at a predefined " +
    "point in the future.")]
public class ReminderCommandHandler : CommandHandlerBase
{
    #region Constants

    private const string REGEX =
        @"^remind ([^\s]+) ((?:to|that) .+) ((?:at|in) (.+)|tomorrow(?: morning)?)$";

    #endregion

    #region Private Members

    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDateTimeStringParser _dateTimeStringParser;
    private readonly IReminderTextGenerator _reminderTextGenerator;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="ReminderCommandHandler"/> class.
    /// </summary>
    public ReminderCommandHandler(
        IBot bot,
        IUnitOfWorkFactory unitOfWorkFactory,
        IDateTimeProvider dateTimeProvider,
        IDateTimeStringParser dateTimeStringParser,
        IReminderTextGenerator reminderTextGenerator) : base(bot)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _dateTimeProvider = dateTimeProvider;
        _dateTimeStringParser = dateTimeStringParser;
        _reminderTextGenerator = reminderTextGenerator;
    }

    #endregion

    #region Private Methods

    private Reminder CreateReminder(
        string reminder,
        User from,
        User to,
        DateTime when,
        IUnitOfWork uow)
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

    private User? LoadTarget(CommandEventArgs commandEventArgs, IUnitOfWork uow, string value)
    {
        return value == "me"
            ? commandEventArgs.From.User
            : uow.GetRepository<IUserRepository, User>().FindByUsername(value);
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc/>
    /// <remarks>
    /// This implementation sets <see cref="Reminder"/>s.
    /// </remarks>
    public override void Handle(CommandEventArgs e)
    {
        var match = new Regex(REGEX).Match(e.Command);
        var targetStr = match.Groups[1].Value;
        var reminder = match.Groups[2].Value;
        var timeStr = match.Groups[3].Value;

        if (!_dateTimeStringParser
                .TryParse(_dateTimeProvider.GetCurrentTime(), timeStr, out var when))
        {
            Bot.ReplyToMessage(e, $"Cannot parse time string '{timeStr}'");
            return;
        }

        using var uow = _unitOfWorkFactory.Build();
        var target = LoadTarget(e, uow, targetStr);

        if (target == null)
        {
            Bot.ReplyToMessage(e, $"Cannot find user with username '{targetStr}'");
            return;
        }

        CreateReminder(reminder, e.From.User!, target, when!.Value, uow);
        uow.Commit();

        Bot.ReplyToMessage(e, $"Reminder set for {when}");
    }

    #endregion
}
