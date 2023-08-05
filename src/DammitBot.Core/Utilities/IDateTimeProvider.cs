using System;

namespace DammitBot.Utilities;

/// <summary>
/// Provides the current <see cref="DateTime"/>.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Get the current <see cref="DateTime"/>.
    /// </summary>
    DateTime GetCurrentTime();
    
    /// <inheritdoc cref="DateTimeExtensions.GetNext"/>
    DateTime GetNext(int hour, int minute = 0, int second = 0);
}