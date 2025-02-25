﻿namespace DammitBot.Metadata;

/// <inheritdoc cref="IHandlesCommandAttribute"/>
public class HandlesCommandAttribute : HandlesMessageAttribute, IHandlesCommandAttribute
{
    #region Properties
    
    /// <inheritdoc />
    public string Description { get; }
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="HandlesCommandAttribute"/> class.
    /// </summary>
    public HandlesCommandAttribute(string rgx, string description, bool adminOnly = false)
        : base(rgx, adminOnly)
    {
        Description = description;
    }
    
    #endregion
}