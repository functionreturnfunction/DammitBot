using System;
using DammitBot.Events;

namespace DammitBot.Library;

/// <summary>
/// Client which does the work of authenticating/connecting to an Irc server, joining channels, and
/// sending/receiving messages.
/// </summary>
public interface IIrcClient
{
    #region Events

    /// <summary>
    /// Event fired when the client is ready to join channels.
    /// </summary>
    event EventHandler? ReadyToJoinChannels;
    /// <summary>
    /// Event fired when a message has been received via a channel. 
    /// </summary>
    event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    
    #endregion
    
    #region Abstract Methods
    
    /// <summary>
    /// Connect to the configured server.
    /// </summary>
    void Connect();
    /// <summary>
    /// Join the specified <paramref name="channel"/> on the server. 
    /// </summary>
    void JoinChannel(string channel);
    /// <summary>
    /// Send the specified <paramref name="message"/> to the specified <see cref="targets"/> (likely to be
    /// channels or other users). 
    /// </summary>
    void SendMessage(string message, params string[] targets);
    
    #endregion
}