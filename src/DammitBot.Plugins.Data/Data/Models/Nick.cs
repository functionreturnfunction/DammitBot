using System;
using DammitBot.Library;

namespace DammitBot.Data.Models;

/// <summary>
/// Represents a user of a protocol who sends messages that the bot observes.  These can be tied to
/// <see cref="User"/> records, potentially affording previously-known protocol users certain privileges,
/// such as the ability to issue commands.
/// </summary>
public class Nick : IEntityWithTimestamps
{
    #region Properties

    /// <inheritdoc/>
    public virtual int Id { get; set; }
    /// <summary>
    /// Nickname of the protocol user.
    /// </summary>
    public virtual required string Nickname { get; set; }
    /// <summary>
    /// Linked bot system user, if applicable.
    /// </summary>
    public virtual User? User { get; set; }
    /// <summary>
    /// Foreign key value linking to the bot system user, if applicable.
    /// </summary>
    public virtual int? UserId { get; set; }

    /// <inheritdoc/>
    public virtual DateTime CreatedAt { get; set; }
    /// <inheritdoc/>
    public virtual DateTime? UpdatedAt { get; set; }

    #endregion
}