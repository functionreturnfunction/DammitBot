namespace DammitBot.Metadata;

/// <inheritdoc cref="IHandlesCommandAttribute"/>
public class HandlesCommandAttribute : HandlesMessageAttribute, IHandlesCommandAttribute
{
    /// <summary>
    /// Constructor for the <see cref="HandlesCommandAttribute"/> class.
    /// </summary>
    public HandlesCommandAttribute(string rgx) : base(rgx) {}
}