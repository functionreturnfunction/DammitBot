using System;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot;

/// <inheritdoc cref="IBot"/>
public class Bot : IBot
{
    #region Constants

    // TODO: Eliminate this
    /// <summary>
    /// Default regex value that the bot will be referred to as.
    /// </summary>
    public const string DEFAULT_GOES_BY = "(?:dammit )?bot";

    #endregion

    #region Private Members

    private readonly IMessageHandlerFactory _handlerFactory;
    private readonly IPluginService _pluginService;
    private readonly IProtocolService _protocolService;
    private readonly ILogger<Bot> _log;

    #endregion

    #region Properties

    /// <inheritdoc cref="IBot.Running"/>
    public bool Running { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="Bot"/> class.
    /// </summary>
    public Bot(
        IMessageHandlerFactory handlerFactory,
        IProtocolService protocolService,
        IPluginService pluginService,
        ILogger<Bot> log)
    {
        _handlerFactory = handlerFactory;
        _protocolService = protocolService;
        _pluginService = pluginService;
        _log = log;
    }

    #endregion

    #region Private Methods

    private void Protocols_ChannelMessageReceived(object? sender, MessageEventArgs e)
    {
        if (_log.IsEnabled(LogLevel.Trace))
        {
            _log.LogTrace(
                "Message received from protocol {Protocol} over channel {Channel} from user " +
                "{User}: '{Message}'",
                e.Protocol,
                e.Channel,
                e.User,
                e.RawMessage);
        }

        _handlerFactory.BuildHandler(e).Handle(e);
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IBot.Run"/>
    public void Run()
    {
        if (Running)
        {
            throw new InvalidOperationException("Bot is already running.");
        }

        _log.LogInformation("Bot is starting up");
        
        _pluginService.Initialize();
        _protocolService.Initialize();
        _protocolService.ChannelMessageReceived += Protocols_ChannelMessageReceived;

        Running = true;
    }

    /// <inheritdoc cref="IBot.SayToAll"/>
    public void SayToAll(string message)
    {
        if (_log.IsEnabled(LogLevel.Trace))
        {
            _log.LogTrace(
                "Broadcasting over all protocols to all channels, message {Message}",
                message);
        }

        _protocolService.SayToAll(message);
    }

    /// <inheritdoc cref="IBot.Die"/>
    public void Die()
    {
        _log.LogInformation("Bot is shutting down");

        Running = false;
    }

    /// <inheritdoc cref="IBot.ReplyToMessage"/>
    public void ReplyToMessage(MessageEventArgs args, string response)
    {
        if (_log.IsEnabled(LogLevel.Trace))
        {
            _log.LogTrace(
                "Responding to message {Original} from protocol {Protocol} over channel " +
                "{Channel} with '{Response}'",
                args.RawMessage,
                args.Protocol,
                args.Channel,
                response);
        }
        
        _protocolService.SayToChannel(args.Protocol, args.Channel, response);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// <remarks>
    /// This implementation cleans up any initialized plugins, detaches from any protocol events, and
    /// disconnects from any connected protocols.
    /// </remarks>
    public void Dispose()
    {
        _pluginService.Cleanup();
        _protocolService.ChannelMessageReceived -= Protocols_ChannelMessageReceived;
        _protocolService.Cleanup();
    }

    #endregion
}