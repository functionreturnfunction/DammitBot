using System.Text.RegularExpressions;
using DammitBot.Data.Models;
using DammitBot.MessageHandlers;

namespace DammitBot.Events;

public class CommandEventArgs : MessageEventArgs
{
    #region Properties

    public virtual string Command { get; }

    public virtual Nick From { get; }

    #endregion

    #region Constructors

    public CommandEventArgs(MessageEventArgs args, Nick from)
        : base(args.Message, args.Channel, args.Protocol, args.User)
    {
        From = from;
    }

    /// <summary>
    /// for testing purposes only!!!
    /// </summary>
    public CommandEventArgs() : this(null, null) {}

    #endregion

    #region Private Methods

    private string ReadCommand(string message)
    {
        return Regex.Match(message, CommandMessageHandler.REGEX).Groups[1].ToString();
    }

    #endregion
}