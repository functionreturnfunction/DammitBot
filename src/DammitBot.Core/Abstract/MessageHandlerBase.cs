using DammitBot.Events;

namespace DammitBot.Abstract;

public abstract class MessageHandlerBase<TEventArgs> : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Abstract Methods

    public abstract void Handle(TEventArgs e);

    #endregion
}