using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols;

/// <inheritdoc />
public class Slack : ISlack
{
    #region Constants

    /// <inheritdoc cref="Name" />
    public const string PROTOCOL_NAME = nameof(Slack);
    
    #endregion
    
    #region Private Members
    
    private readonly ISlackClientFactory _slackClientFactory;
    private readonly ILogger<Slack> _log;
    private ISlackClient? _slack;
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    public string Name => PROTOCOL_NAME;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="Slack"/> class.
    /// </summary>
    public Slack(ISlackClientFactory slackClientFactory, ILogger<Slack> log)
    {
        _slackClientFactory = slackClientFactory;
        _log = log;
    }
    
    #endregion
    
    #region Event Handlers

    private void Slack_MessageReceived(object? sender, MessageEventArgs e)
    {
        _log.LogReceivedMessage(e);
        ChannelMessageReceived?.Invoke(sender, e);
    }
    
    #endregion
    
    #region Events/Delegates

    /// <inheritdoc />
    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    /// <remarks>
    /// This implementation wires up some event handling onto the <see cref="ISlackClient"/>, and then
    /// uses it to connect to the slack API.
    /// </remarks>
    public void Initialize()
    {
        _log.LogInformation("Initiating slack client");

        _slack = _slackClientFactory.Build();
        _slack.MessageReceived += Slack_MessageReceived;
        _slack.Connect();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation disposes of the wrapped <see cref="ISlackClient"/> implementation.
    /// </remarks>
    public void Cleanup()
    {
        if (_slack == null)
        {
            throw new InvalidOperationException(
                "Cannot cleanup a protocol which hasn't been initialized");
        }
        
        _slack.Dispose();
    }
    
    /// <inheritdoc />
    public void SayToAll(string message)
    {
        if (_slack == null)
        {
            throw new InvalidOperationException(
                $"A {nameof(Slack)} instance cannot be used before {nameof(Initialize)} has " +
                "been called on it");
        }
            
        _slack!.SendMessage(message);
    }

    /// <inheritdoc />
    public void SayToChannel(string channel, string message)
    {
        if (_slack == null)
        {
            throw new InvalidOperationException(
                $"A {nameof(Slack)} instance cannot be used before {nameof(Initialize)} has " +
                "been called on it");
        }
            
        _slack!.SendMessage(message, channel);
    }
    
    #endregion
}