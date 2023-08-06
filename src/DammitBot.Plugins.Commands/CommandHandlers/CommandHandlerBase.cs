using DammitBot.Events;

namespace DammitBot.CommandHandlers;

/// <inheritdoc />
public abstract class CommandHandlerBase : ICommandHandler
{
    #region Properties

    /// <summary>
    /// <see cref="IBot"/> instance to interact with as-needed, depending on the nature of the command(s)
    /// being handled.
    /// </summary>
    protected IBot Bot { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandHandlerBase"/> class.
    /// </summary>
    /// <param name="bot"></param>
    protected CommandHandlerBase(IBot bot)
    {
        Bot = bot;
    }

    #endregion

    #region Abstract Methods

    /// Handle a command event represented by <paramref name="e"/>.
    public abstract void Handle(CommandEventArgs e);

    #endregion
}