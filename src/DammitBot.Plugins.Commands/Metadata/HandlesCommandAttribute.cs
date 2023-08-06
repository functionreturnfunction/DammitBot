namespace DammitBot.Metadata;

/// <inheritdoc cref="IHandlesCommandAttribute"/>
public class HandlesCommandAttribute : HandlesMessageAttribute, IHandlesCommandAttribute
{
    /// <inheritdoc />
    public string Description { get; }

    /// <summary>
    /// Constructor for the <see cref="HandlesCommandAttribute"/> class.
    /// </summary>
    public HandlesCommandAttribute(string rgx, string description) : base(rgx)
    {
        Description = description;
    }
}