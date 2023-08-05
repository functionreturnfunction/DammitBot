using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Factory for creating instances of <typeparamref name="TMessageHandler"/> which handle messages
/// represented by instances of <typeparamref name="TEventArgs"/>.
/// </summary>
public interface IMessageHandlerFactory<out TMessageHandler, in TEventArgs>
    where TMessageHandler : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Abstract Methods

    /// <summary>
    /// Build and return an instance of <typeparamref name="TMessageHandler"/> which handles the message
    /// represented by <typeparamref name="TEventArgs"/> <paramref name="message"/>.
    /// </summary>
    TMessageHandler BuildHandler(TEventArgs message);

    #endregion
}