namespace DateTimeProvider;

/// <summary>
/// Provides the current <see cref="DateTime"/>.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Get the current <see cref="DateTime"/>.
    /// </summary>
    DateTime GetCurrentTime();
}