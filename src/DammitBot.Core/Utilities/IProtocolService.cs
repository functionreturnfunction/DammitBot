using System;
using System.Collections.Generic;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Utilities;

/// <summary>
/// Service providing instances of <see cref="IProtocol"/>, as well as convenience methods for sending
/// messages to them, and events triggered by them.
/// </summary>
public interface IProtocolService : IPluginThingy
{
    #region Abstract Properties
    
    /// <summary>
    /// Instances of all available concrete implementations of <see cref="IProtocol"/>.
    /// </summary>
    IEnumerable<IProtocol> Thingies { get; }
    
    #endregion
    
    #region Abstract Events

    event EventHandler<MessageEventArgs> ChannelMessageReceived;
    
    #endregion
    
    #region Abstract Methods
    
    void SayToAll(string message);
    void SayToChannel(string protocol, string channel, string message);
    
    #endregion
}