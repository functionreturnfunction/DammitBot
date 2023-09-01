using System.Diagnostics.CodeAnalysis;
using DammitBot.Configuration;
using DammitBot.Library;
using Microsoft.Extensions.Options;
using SlackNet;

namespace DammitBot.Wrappers;

/// <summary>
/// <see cref="ISlackClientFactory"/> implementation which wraps a <see cref="SlackServiceBuilder"/>
/// instance for the purposes of instantiating SlackNet services wrapped as a <see cref="ISlackClient"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class SlackServiceBuilderWrapper : ISlackClientFactory
{
    #region Private Members
    
    private readonly SlackServiceBuilder _slackServiceBuilder;
    private readonly SlackConfiguration _config;
    private readonly SlackNetLoggerWrapper _log;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SlackServiceBuilder"/> class.
    /// </summary>
    public SlackServiceBuilderWrapper(
        SlackServiceBuilder slackServiceBuilder,
        IOptions<SlackConfiguration> config,
        SlackNetLoggerWrapper log)
    {
        _slackServiceBuilder = slackServiceBuilder;
        _config = config.Value;
        _log = log;
    }
    
    #endregion
    
    #region Exposed Methods
    
    /// <inheritdoc />
    public ISlackClient Build()
    {
        var builder = _slackServiceBuilder
            .UseLogger(_ => _log)
            .UseApiToken(_config.ApiToken)
            .UseAppLevelToken(_config.AppLevelToken);

        return new SlackClientWrapper(builder);
    }
    
    #endregion
}