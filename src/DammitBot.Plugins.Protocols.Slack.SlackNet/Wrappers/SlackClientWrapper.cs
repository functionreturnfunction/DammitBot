using DammitBot.Events;
using DammitBot.Library;
using SlackNet;
using SlackNet.Events;

namespace DammitBot.Wrappers;

/// <inheritdoc cref="ISlackClient"/>
public class SlackClientWrapper : ISlackClient, IEventHandler<MessageEvent>
{
    #region Private Members
    
    private readonly ISlackSocketModeClient _socketClient;
    private readonly ISlackApiClient _apiClient;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SlackClientWrapper"/> class.
    /// </summary>
    /// <param name="builder"></param>
    public SlackClientWrapper(SlackServiceBuilder builder)
    {
        builder.RegisterEventHandler(ctx => this);
        _socketClient = builder.GetSocketModeClient();
        _apiClient = builder.GetApiClient();
    }
    
    #endregion
    
    #region Events/Delegates

    /// <inheritdoc />
    public event EventHandler<MessageEventArgs>? MessageReceived;
    
    #endregion

    #region Public Methods
    
    /// <inheritdoc />
    /// <remarks>
    /// This implementation initiates a connection to the Slack API using via a
    /// <see cref="ISlackSocketModeClient"/> instance.
    /// </remarks>
    public void Connect()
    {
        _socketClient.Connect();
    }

    /// <inheritdoc />
    public void SendMessage(string message, params string[] targets)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Implementation of <see cref="IEventHandler{MessageEvent}.Handle"/> which wraps the
    /// <paramref name="messageEvent"/> and bubbles it upward.
    /// </summary>
    public Task Handle(MessageEvent messageEvent)
    {
        MessageReceived?.Invoke(_socketClient, new MessageEventWrapper(messageEvent));
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation disconnects and disposes of the <see cref="ISlackSocketModeClient"/> instance.
    /// </remarks>
    public void Dispose()
    {
        if (_socketClient.Connected)
        {
            _socketClient.Disconnect();
        }

        _socketClient.Dispose();
    }
    
    #endregion
}