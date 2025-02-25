﻿using System;
using DammitBot.Library;

namespace DammitBot.Data.Models;

/// <summary>
/// Represents a previously-known bot system user.  Can be linked to many <see cref="Nick"/>s across
/// multiple protocols.
/// </summary>
public class User : IEntityWithTimestamps
{
    #region Properties

    /// <inheritdoc/>
    public virtual int Id { get; set; }
    /// <summary>
    /// Username of the bot system user.
    /// </summary>
    public virtual required string Username { get; set; }
    /// <summary>
    /// Boolean value indicating whether or not the user is an admin which can do special things that
    /// non-admins can't.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <inheritdoc/>
    public virtual DateTime CreatedAt { get; set; }
    /// <inheritdoc/>
    public virtual DateTime? UpdatedAt { get; set; }

    #endregion
}