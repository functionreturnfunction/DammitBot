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

    /// <summary>
    /// Event fired when a message comes in from a particular channel via a particular protocol.
    /// </summary>
    event EventHandler<MessageEventArgs> ChannelMessageReceived;
    
    #endregion
    
    #region Abstract Methods
    
    /// <summary>
    /// Send the provided <paramref name="message"/> via all available/connected protocols to all
    /// configured/available channels.
    /// </summary>
    void SayToAll(string message);
    
    /// <summary>
    /// Send the provided <paramref name="message"/> via the specified <paramref name="protocol"/> to the
    /// specified <paramref name="channel"/>.
    /// </summary>
    void SayToChannel(string protocol, string channel, string message);
    
    #endregion
}