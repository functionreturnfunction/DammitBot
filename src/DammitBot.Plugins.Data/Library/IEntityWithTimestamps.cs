using System;

namespace DammitBot.Library;

/// <summary>
/// Represents a persisted data object which keeps track of the <see cref="DateTime"/> at which it was
/// created, as well as the <see cref="DateTime"/> at which it was last updated.
/// </summary>
public interface IEntityWithTimestamps : IEntity
{
    /// <summary>
    /// <see cref="DateTime"/> at which the entity was first created (and stored to persistence).
    /// </summary>
    DateTime CreatedAt { get; set; }
    /// <summary>
    /// <see cref="DateTime"/> at which the entity was last updated (and stored to persistence).
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}