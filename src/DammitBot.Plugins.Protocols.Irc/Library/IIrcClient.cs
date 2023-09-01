using System;
using DammitBot.Abstract;

namespace DammitBot.Library;

/// <summary>
/// Client which does the work of authenticating/connecting to an Irc server, joining channels, and
/// sending/receiving messages.
/// </summary>
public interface IIrcClient : IProtocolClient, IDisposable
{
    #region Events

    /// <summary>
    /// Event fired when the client is ready to join channels.
    /// </summary>
    event EventHandler? ReadyToJoinChannels;
    /// <summary>
    /// Event fired when the client fails to connect to the server.
    /// </summary>
    public event EventHandler<IIrcErrorEventArgs>? ConnectionFailed; 
    /// <summary>
    /// Event fired when the client receives an error message from the server.
    /// </summary>
    public event EventHandler<IIrcErrorEventArgs>? ErrorMessageReceived; 
    
    #endregion
    
    #region Abstract Methods
    
    /// <summary>
    /// Join the specified <paramref name="channel"/> on the server. 
    /// </summary>
    void JoinChannel(string channel);
    
    #endregion
}