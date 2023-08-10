using System;
using DammitBot.Library;

namespace DammitBot.Data.Models;

/// <summary>
/// Represents a reminder that was recorded by a user to save a message to be sent to a chanel or user at
/// a predefined point in the future.
/// </summary>
public class Reminder : IEntityWithTimestamps
{
    #region Properties

    /// <inheritdoc/>
    public virtual int Id { get; set; }
    /// <summary>
    /// Text of the message to send.
    /// </summary>
    public virtual string Text { get; set; }
    /// <summary>
    /// Point in time at which the reminder should be sent.
    /// </summary>
    public virtual DateTime RemindAt { get; set; }
    /// <summary>
    /// Actual point in time at which the reminder was sent, if it was.
    /// </summary>
    public virtual DateTime? RemindedAt { get; set; }
    /// <inheritdoc/>
    public virtual DateTime CreatedAt { get; set; }
    /// <inheritdoc/>
    public virtual DateTime? UpdatedAt { get; set; }
    /// <summary>
    /// <see cref="User"/> who set the reminder.
    /// </summary>
    public virtual User From { get; set; }
    /// <summary>
    /// Id of the <see cref="User"/> who set the reminder.
    /// </summary>
    public virtual int? FromId { get; set; }
    /// <summary>
    /// <see cref="User"/> who is the intended recipient of the reminder.
    /// </summary>
    public virtual User To { get; set; }
    /// <summary>
    /// Id of the <see cref="User"/> who is the intended recipient of the reminder.
    /// </summary>
    public virtual int? ToId { get; set; }

    #endregion
}