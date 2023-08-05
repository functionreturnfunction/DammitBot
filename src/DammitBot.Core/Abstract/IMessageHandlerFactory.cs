using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Factory for creating instances of <typeparamref name="THandler"/> which handle messages represented
/// by instances of <typeparamref name="TEventArgs"/>.
/// </summary>
public interface IMessageHandlerFactory<out THandler, in TEventArgs>
    where THandler : IMessageHandler<TEventArgs>
    where TEventArgs : MessageEventArgs
{
    #region Abstract Methods

    /// <summary>
    /// Build and return an instance of <typeparamref name="THandler"/> which handles the message
    /// represented by <typeparamref name="TEventArgs"/> <paramref name="message"/>.
    /// </summary>
    THandler BuildHandler(TEventArgs message);

    #endregion
}