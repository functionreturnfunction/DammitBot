namespace DateTimeProvider;

/// <inheritdoc cref="IDateTimeProvider"/>
/// <remarks>
/// This implementation will always provide a static <see cref="DateTime"/> value which is set via its
/// constructor.
/// </remarks>
public class TestDateTimeProvider : SystemClockDateTimeProvider
{
    #region Private Members

    private readonly DateTime _now;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="TestDateTimeProvider"/> class.
    /// </summary>
    public TestDateTimeProvider(DateTime now)
    {
        _now = now;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IDateTimeProvider.GetCurrentTime"/>
    /// <remarks>
    /// This implementation will always provide a static <see cref="DateTime"/> value which is set via
    /// this class' constructor.
    /// </remarks>
    public override DateTime GetCurrentTime()
    {
        return _now;
    }

    #endregion
}