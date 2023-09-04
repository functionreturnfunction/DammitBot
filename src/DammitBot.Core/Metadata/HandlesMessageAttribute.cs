using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DammitBot.Metadata;

/// <inheritdoc cref="IHandlesMessageAttribute"/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
[DebuggerDisplay("Handles message '{Regex}'{AdminOnly ? \" Admin Only\" : null}")]
public class HandlesMessageAttribute : Attribute, IHandlesMessageAttribute
{
    #region Properties

    /// <inheritdoc cref="IHandlesMessageAttribute.Regex"/>
    public Regex Regex { get; }
    
    /// <inheritdoc cref="IHandlesMessageAttribute.AdminOnly"/>
    public bool AdminOnly { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="HandlesMessageAttribute"/> class.
    /// </summary>
    public HandlesMessageAttribute(string rgx, bool adminOnly = false)
    {
        Regex = new Regex(rgx);
        AdminOnly = adminOnly;
    }

    #endregion
}