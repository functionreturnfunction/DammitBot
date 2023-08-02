using DammitBot.Events;

namespace DammitBot.Abstract;

public interface IMessageHandler<in TArgs>
    where TArgs : MessageEventArgs
{
    #region Abstract Methods

    void Handle(TArgs e);

    #endregion
}