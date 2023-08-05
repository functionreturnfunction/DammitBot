using System;
using DammitBot.Protocols.Irc;
using IrcDotNet;

namespace DammitBot.Events;

public class IrcMessageEventArgs : MessageEventArgs
{
    #region Constructors

    public IrcMessageEventArgs(IrcRawMessageEventArgs args)
        : base(ParseMessage(args), ParseChannel(args), Irc.PROTOCOL_NAME, ParseNick(args)) {}

    #endregion
        
    #region Private Methods

    private static string ParseChannel(IrcRawMessageEventArgs args)
    {
        return args.Message.Parameters[0];
    }

    private static string ParseMessage(IrcRawMessageEventArgs args)
    {
        return args.Message.Parameters[1];
    }

    private static string ParseNick(IrcRawMessageEventArgs args)
    {
        return args.Message.Source.Name;
    }

    #endregion
}