using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public abstract class CommandHandlerBase : ICommandHandler
{
    #region Private Members

    protected readonly IBot _bot;

    #endregion

    #region Constructors

    public CommandHandlerBase(IBot bot)
    {
        _bot = bot;
    }

    #endregion

    #region Abstract Methods

    public abstract void Handle(CommandEventArgs e);

    #endregion
}