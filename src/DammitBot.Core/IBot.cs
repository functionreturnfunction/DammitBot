using System;
using DammitBot.Events;

namespace DammitBot;

public interface IBot : IDisposable
{
    #region Abstract Properties

    bool Running { get; }

    #endregion

    #region Abstract Methods

    void Run();
    void SayToAll(string message);
    void Die();

    void ReceiveMessage(MessageEventArgs args);

    void ReplyToMessage(MessageEventArgs args, string response);

    #endregion
}