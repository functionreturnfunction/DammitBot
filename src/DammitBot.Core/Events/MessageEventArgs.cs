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
    /// The raw text of message being observed and/or handled.
    /// </summary>
    public string RawMessage { get; }
    /// <summary>
    /// Specific channel from which the message originated.
    /// </summary>
    public string Channel { get; }
    /// <summary>
    /// Specific protocol from which the message originated.
    /// </summary>
    public string Protocol { get; }
    /// <summary>
    /// Entity who sent the message.
    /// </summary>
    public string User { get; }

    /// <summary>
    /// Boolean value indicating whether or not the user who sent the message is an admin.
    /// </summary>
    /// <remarks>
    /// This implementation will always return false, because basic messages have no way of telling if
    /// they were sent by an admin user.
    /// </remarks>
    public virtual bool UserIsAdmin => false;
    /// <summary>
    /// The text of the message being handled, after being filtered for a specific purpose.
    /// </summary>
    /// <remarks>
    /// This implementation returns the <see cref="RawMessage"/>.
    /// </remarks>
    public virtual string? Message => RawMessage;

    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageEventArgs"/> class.
    /// </summary>
    public MessageEventArgs(string rawMessage, string channel, string protocol, string user)
    {
        RawMessage = rawMessage;
        Channel = channel;
        Protocol = protocol;
        User = user;
    }
    
    #endregion
}