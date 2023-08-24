namespace DateTimeProvider;


/// <inheritdoc cref="IDateTimeProvider" />
/// <remarks>This implementation uses the system clock via <see cref="DateTime.Now"/>.</remarks>
public class SystemClockDateTimeProvider : IDateTimeProvider
{
    #region Exposed Methods

    /// <inheritdoc cref="IDateTimeProvider.GetCurrentTime"/>
    public virtual DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    #endregion
}