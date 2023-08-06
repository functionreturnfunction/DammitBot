using DammitBot.Events;
using Microsoft.Extensions.Logging;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="ILogger"/> interface.
/// </summary>
public static class ILoggerExtensions
{
    /// <summary>
    /// Log the received message represented by <paramref name="args"/>.
    /// </summary>
    public static void LogReceivedMessage(this ILogger log, MessageEventArgs args)
    {
        log.LogDebug("Message received: '{Message}'", args.Message);
    }
}