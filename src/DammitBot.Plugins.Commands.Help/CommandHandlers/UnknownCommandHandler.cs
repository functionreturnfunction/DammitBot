using DammitBot.Configuration;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public class UnknownCommandHandler : CommandHandlerBase
{
    #region Constants

    public const string MESSAGE =
        "Sorry, I don't understand you.  Say '{0} help' for a list of help commands.";

    #endregion

    #region Private Members

    private readonly IBotConfigurationSection _botConfig;

    #endregion

    #region Constructors

    public UnknownCommandHandler(IBot bot, IConfigurationManager configurationManager) : base(bot)
    {
        _botConfig = configurationManager.BotConfig;
    }

    #endregion

    #region Exposed Methods

    public override void Handle(CommandEventArgs e)
    {
        _bot.ReplyToMessage(e, string.Format(MESSAGE, _botConfig.GoesBy));
    }

    #endregion
}