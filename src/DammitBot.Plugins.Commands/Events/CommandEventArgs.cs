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

    /// <inheritdoc />
    /// <remarks>
    /// This implementation checks the <see cref="Nick"/> <see cref="From"/> property to see if it has a
    /// <see cref="User"/>, and if that <see cref="User"/> is an admin (<see cref="User.IsAdmin"/>).
    /// </remarks>
    public override bool UserIsAdmin => From.User?.IsAdmin ?? false;
    /// <inheritdoc />
    /// <remarks>
    /// This implementation returns the filtered <see cref="Command"/>.
    /// </remarks>
    public override string? Message => Command;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandEventArgs"/> class.
    /// </summary>
    public CommandEventArgs(MessageEventArgs args, string command, Nick from)
        : base(args.RawMessage, args.Channel, args.Protocol, args.User)
    {
        Command = command;
        From = from;
    }

    #endregion
}