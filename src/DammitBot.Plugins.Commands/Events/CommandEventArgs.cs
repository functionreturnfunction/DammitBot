using DammitBot.Data.Models;

namespace DammitBot.Events;

/// <summary>
/// <see cref="MessageEventArgs"/> implementation providing the text of a command and the
/// <see cref="Nick"/> representing whoever issued the command.
/// </summary>
public class CommandEventArgs : MessageEventArgs
{
    #region Properties

    /// <summary>
    /// String containing the command.
    /// </summary>
    public virtual string Command { get; }

    /// <summary>
    /// <see cref="Nick"/> representing whoever issued the command.
    /// </summary>
    public virtual Nick From { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandEventArgs"/> class.
    /// </summary>
    public CommandEventArgs(MessageEventArgs args, string command, Nick from)
        : base(args.Message, args.Channel, args.Protocol, args.User)
    {
        Command = command;
        From = from;
    }

    #endregion
}