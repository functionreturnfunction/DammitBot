using Microsoft.Extensions.Logging;
using SlackNet;
using ILogger = SlackNet.ILogger;

namespace DammitBot.Wrappers;

/// <summary>
/// Implementation of the SlackNet <see cref="ILogger"/> interface which wraps a
/// <see cref="ILogger{SlackServiceBuilder}"/> instance to facilitate logging for the internals of
/// SlackNet's services.
/// </summary>
public class SlackNetLoggerWrapper : ILogger
{
    #region Private Members
    
    private readonly ILogger<SlackServiceBuilder> _log;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SlackNetLoggerWrapper"/> class.
    /// </summary>
    public SlackNetLoggerWrapper(ILogger<SlackServiceBuilder> log)
    {
        _log = log;
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />    
    public void Log(ILogEvent logEvent)
    {
        switch (logEvent.Category)
        {
            case LogCategory.Data:
                _log.LogDebug(logEvent.FullMessage());
                break;
            case LogCategory.Internal:
                _log.LogTrace(logEvent.FullMessage());
                break;
            case LogCategory.Error:
                _log.LogError(logEvent.Exception, logEvent.FullMessage());
                break;
            case LogCategory.Request:
                _log.LogDebug(logEvent.FullMessage());
                break;
            case LogCategory.Serialization:
                break;
            default:
                throw new NotImplementedException(
                    "Not sure how to handle uncategorized log messages");
        }
    }
    
    #endregion
}