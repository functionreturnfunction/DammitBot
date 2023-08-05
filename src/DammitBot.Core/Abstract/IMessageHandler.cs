using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Handler of message events represented by <typeparamref name="TArgs"/>. 
/// </summary>
public interface IMessageHandler<in TArgs>
    where TArgs : MessageEventArgs
{
    #region Abstract Methods

    /// <summary>
    /// Handle a message event represented by <paramref name="e"/>.
    /// </summary>
    void Handle(TArgs e);

    #endregion
}