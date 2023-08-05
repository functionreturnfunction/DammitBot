using System;

namespace DammitBot.Events;

/// <summary>
/// <see cref="EventArgs"/> implementation representing a basic message observed and/or handled by the
/// bot.  Inheritors can interpret the various properties to suit their specific needs.
/// </summary>
public class MessageEventArgs : EventArgs
{
    #region Properties

    /// <summary>
    /// The message being observed and/or handled.
    /// </summary>
    public virtual string Message { get; }
    /// <summary>
    /// Specific channel from which the message originated.
    /// </summary>
    public virtual string Channel { get; }
    /// <summary>
    /// Specific protocol from which the message originated.
    /// </summary>
    public virtual string Protocol { get; }
    /// <summary>
    /// Entity who sent the message.
    /// </summary>
    public virtual string User { get; }

    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageEventArgs"/> class.
    /// </summary>
    public MessageEventArgs(string message, string channel, string protocol, string user)
    {
        Message = message;
        Channel = channel;
        Protocol = protocol;
        User = user;
    }
    
    #endregion
}