using DammitBot.Events;

namespace DammitBot.Abstract
{
    public interface IHandlerFactory<out THandler, in TEventArgs>
        where THandler : IMessageHandler<TEventArgs>
        where TEventArgs : MessageEventArgs
    {
        #region Abstract Methods

        THandler BuildHandler(TEventArgs message);

        #endregion
    }
}
