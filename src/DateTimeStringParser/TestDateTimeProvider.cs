using System;

namespace DateTimeStringParser;

public class TestDateTimeProvider : DateTimeProvider
{
    #region Private Members

    protected DateTime _now;

    #endregion

    #region Constructors

    public TestDateTimeProvider(DateTime now)
    {
        SetCurrentTime(now);
    }

    #endregion

    #region Exposed Methods

    public virtual void SetCurrentTime(DateTime now)
    {
        _now = now;
    }

    public override DateTime GetCurrentTime()
    {
        return _now;
    }

    #endregion
}