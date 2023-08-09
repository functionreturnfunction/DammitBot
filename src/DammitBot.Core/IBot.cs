using System;
using DammitBot.Events;

namespace DammitBot;

/// <summary>
/// The bot itself, which does nothing on its own except gather and initialize plugins.
/// </summary>
public interface IBot : IDisposable
{
    #region Abstract Properties

    /// <summary>
    /// Boolean value indicating whether or not the bot is running.
    /// </summary>
    bool Running { get; }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Start up the bot.
    /// </summary>
    void Run();
    /// <summary>
    /// Send the supplied <paramref name="message"/> to all connected/available protocols.
    /// </summary>
    void SayToAll(string message);
    /// <summary>
    /// Change the bot <see cref="Running"/> state to false so it will exit.
    /// </summary>
    void Die();
    /// <summary>
    /// Respond to the message represented by <paramref name="args"/> at its originating
    /// protocol and channel, with the supplied <paramref name="response"/>.
    /// </summary>
    void ReplyToMessage(MessageEventArgs args, string response);

    #endregion
}