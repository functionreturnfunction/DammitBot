using DammitBot.Events;
using DammitBot.Protocols;
using IrcDotNet;

namespace DammitBot.Wrappers;

/// <summary>
/// <see cref="MessageEventArgs"/> wrapper/adapter around IrcDotNet's <see cref="IrcRawMessageEventArgs"/>
/// class.
/// </summary>
public class IrcRawMessageEventArgsWrapper : MessageEventArgs
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcRawMessageEventArgsWrapper"/> class.
    /// </summary>
    public IrcRawMessageEventArgsWrapper(IrcRawMessageEventArgs args)
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