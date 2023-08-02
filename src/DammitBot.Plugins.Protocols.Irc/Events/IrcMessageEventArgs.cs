using System.Text.RegularExpressions;
using DammitBot.Protocols.Irc;
using DammitBot.Wrappers;

namespace DammitBot.Events;

public class IrcMessageEventArgs : MessageEventArgs
{
    #region Constructors

    public IrcMessageEventArgs(PrivateMessageEventArgsWrapper args)
        : base(
            args.PrivateMessage.Message,
            ParseChannel(args),
            Irc.PROTOCOL_NAME,
            args.PrivateMessage.Nick) {}
        
    #endregion
        
    #region Private Methods

    private static string ParseChannel(PrivateMessageEventArgsWrapper args)
    {
        return Regex.Match(args.IrcMessage.RawMessage, @"PRIVMSG ([^\s]+) :").Groups[1].Value;
    }

    #endregion
}