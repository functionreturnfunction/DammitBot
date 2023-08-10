using System;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <inheritdoc cref="IPluginThingy" />
/// <remarks>This interface is meant to be used specifically for communication protocols.</remarks>
public interface IProtocol : IPluginThingy
{
    #region Abstract Properties

    /// <summary>
    /// Name of the protocol implemented.
    /// </summary>
    string Name { get; }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Send the provided <paramref name="message"/> via this protocol to all configured/available
    /// channels.
    /// </summary>
    void SayToAll(string message);
    /// <summary>
    /// Send the provided <paramref name="message"/> via this protocol to the specified
    /// <paramref name="channel"/>.
    /// </summary>
    void SayToChannel(string channel, string message);

    #endregion

    #region Events/Delegates

    /// <summary>
    /// Event fired when a message is received via this protocol over a given channel (or from a given
    /// user, etc.).
    /// </summary>
    event EventHandler<MessageEventArgs> ChannelMessageReceived;

    #endregion
}