using System;
using DammitBot.Events;

namespace DammitBot.Abstract;

/// <summary>
/// Client which does the work of authenticating/connecting to a messaging protocol server and
/// sending/receiving messages.
/// </summary>
public interface IProtocolClient
{
    #region Events
    
    /// <summary>
    /// Event fired when a message has been received via a channel or from a user.
    /// </summary>
    event EventHandler<MessageEventArgs>? MessageReceived;

    #endregion
    
    #region Abstract Methods

    /// <summary>
    /// Connect to the configured server/service.
    /// </summary>
    void Connect();

    /// <summary>
    /// Send the specified <paramref name="message"/> to the specified <paramref name="targets"/> (likely
    /// to be channels or other users). 
    /// </summary>
    void SendMessage(string message, params string[] targets);
    
    #endregion
}