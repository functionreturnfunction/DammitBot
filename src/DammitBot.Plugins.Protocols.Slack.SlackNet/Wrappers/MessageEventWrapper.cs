using DammitBot.Events;
using DammitBot.Protocols;
using SlackNet.Events;

namespace DammitBot.Wrappers;

/// <summary>
/// Summary <see cref="MessageEventArgs"/> wrapper/adapter around SlackNet's <see cref="MessageEvent"/>
/// class.
/// </summary>
public class MessageEventWrapper : MessageEventArgs
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageEventWrapper"/> class.
    /// </summary>
    public MessageEventWrapper(MessageEventBase messageEvent)
        : base(messageEvent.Text, messageEvent.Channel, nameof(Slack), messageEvent.User) { }
    
    #endregion
}