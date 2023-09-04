using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.CommandHandlers;

/// <summary>
/// <see cref="ICommandHandler"/> implementation which provides the "die" command, which will cause the
/// bot to disconnect from any connected protocols, stop any running services, and shut down.
/// </summary>
[HandlesCommand(
    "^die$",
    "Disconnect from any connected protocols, stop any running services, and shut down the bot.",
    true)]
public class DieCommandHandler : CommandHandlerBase
{
    #region Constants

    /// <summary>
    /// Farewell message to be sent via all available/connected protocols over all available/registered
    /// channels prior to shutting down the bot. 
    /// </summary>
    public const string MESSAGE = "Alright fine.";

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DieCommandHandler"/> class.
    /// </summary>
    public DieCommandHandler(IBot bot) : base(bot) {}

    #endregion

    #region Exposed Methods

    /// <summary>
    /// Send a farewell <see cref="MESSAGE"/> via all available/connected protocols over all
    /// available/registered channels, and then shut down the bot.
    /// </summary>
    public override void Handle(CommandEventArgs e)
    {
        Bot.SayToAll(MESSAGE);
        Bot.Die();
    }

    #endregion
}