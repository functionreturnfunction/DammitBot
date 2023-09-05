using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace DammitBot.Library;

public class MockLogger<T> : Mock<ILogger<T>>, ILogger<T>
{
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        Object.Log(logLevel, eventId, state, exception, formatter);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return Object.IsEnabled(logLevel);
    }

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return Object.BeginScope(state);
    }
}