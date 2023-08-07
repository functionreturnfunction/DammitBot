namespace DammitBot.Library;

/// <summary>
/// Represents a persisted data object with a primary key <see cref="Id"/>.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// The primary key field.
    /// </summary>
    int Id { get; set; }
}