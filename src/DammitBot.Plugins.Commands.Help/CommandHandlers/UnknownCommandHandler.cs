using DammitBot.Configuration;
using DammitBot.Events;
using Microsoft.Extensions.Options;

namespace DammitBot.CommandHandlers;

/// <summary>
/// <see cref="ICommandHandler"/> implementation which handles apparent commands which cannot be handled
/// by any other commands available.  Lets the user know how to get further assistance with commands.
/// </summary>
public class UnknownCommandHandler : CommandHandlerBase
{
    #region Constants

    /// <summary>
    /// Message used to tell the user their command was not understood, and how they can get further
    /// assistance.
    /// </summary>
    public const string MESSAGE =
        "Sorry, I don't understand you.  Say '{0} help' for a list of help commands.";

    #endregion

    #region Private Members

    private readonly BotConfiguration _botConfig;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="UnknownCommandHandler"/> class.
    /// </summary>
    public UnknownCommandHandler(IBot bot, IOptions<BotConfiguration> botConfig) : base(bot)
    {
        _botConfig = botConfig.Value;
    }

    #endregion

    #region Exposed Methods

    /// <summary>
    /// Respond that the command was not understood, with further assistance.
    /// </summary>
    public override void Handle(CommandEventArgs e)
    {
        Bot.ReplyToMessage(e, string.Format(MESSAGE, _botConfig.GoesBy));
    }

    #endregion
}