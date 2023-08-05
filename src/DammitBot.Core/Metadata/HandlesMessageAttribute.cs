using System;
using System.Text.RegularExpressions;

namespace DammitBot.Metadata;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HandlesMessageAttribute : Attribute, IHandlesMessageAttribute
{
    #region Private Members

    private readonly string _rgx;

    #endregion

    #region Properties

    public Regex Regex => new Regex(_rgx);

    #endregion

    #region Constructors

    public HandlesMessageAttribute(string rgx)
    {
        _rgx = rgx;
    }

    #endregion
}