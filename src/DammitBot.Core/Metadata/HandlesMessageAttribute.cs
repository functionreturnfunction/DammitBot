using System;
using System.Text.RegularExpressions;

namespace DammitBot.Metadata;

/// <inheritdoc cref="IHandlesMessageAttribute"/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HandlesMessageAttribute : Attribute, IHandlesMessageAttribute
{
    #region Private Members

    private readonly string _rgx;

    #endregion

    #region Properties

    /// <inheritdoc cref="IHandlesMessageAttribute.Regex"/>
    public Regex Regex => new(_rgx);

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="HandlesMessageAttribute"/> class.
    /// </summary>
    /// <param name="rgx"></param>
    public HandlesMessageAttribute(string rgx)
    {
        _rgx = rgx;
    }

    #endregion
}