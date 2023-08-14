using System;
using DammitBot.Library;

namespace DammitBot.Data.Models;

/// <summary>
/// Represents a message sent by a user via a specific protocol over a specific channel (or perhaps a
/// private/direct message if the protocol in question supports those).
/// </summary>
public class Message : IEntityWithTimestamps
{
    #region Properties

    /// <inheritdoc/>
    public virtual int Id { get; set; }
    /// <summary>
    /// Text of the message.
    /// </summary>
    public virtual required string Text { get; set; }
    /// <summary>
    /// Protocol via which the message was sent.
    /// </summary>
    public virtual required string Protocol { get; set; }
    /// <summary>
    /// Channel over which the message was sent. 
    /// </summary>
    // TODO: figure out what this value is for private/direct messages and document it
    public virtual required string Channel { get; set; }
    /// <summary>
    /// <see cref="Nick"/> representing the protocol user who sent the message.
    /// </summary>
    public virtual required Nick From { get; set; }
    /// <summary>
    /// Foreign key value linking to the <see cref="Nick"/> representing the protocol user who sent the
    /// message.
    /// </summary>
    public virtual int? FromId { get; set; }

    /// <inheritdoc/>
    public virtual DateTime CreatedAt { get; set; }
    /// <inheritdoc/>
    public virtual DateTime? UpdatedAt { get; set; }

    #endregion
}