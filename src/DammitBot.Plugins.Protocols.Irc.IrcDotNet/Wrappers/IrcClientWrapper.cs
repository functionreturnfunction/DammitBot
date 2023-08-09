using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Library;
using IrcDotNet;
using Microsoft.Extensions.Logging;

namespace DammitBot.Wrappers;

/// <inheritdoc />
/// <remarks>
/// This implementation serves as a wrapper/adapter for IrcDotNet's <see cref="StandardIrcClient"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
public class IrcClientWrapper : IIrcClient
{
    #region Private Members

    private readonly IIrcConfigurationSection _config;
    private readonly ILogger<IrcClientWrapper> _log;
    private readonly StandardIrcClient _innerClient;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcClientWrapper"/> class.
    /// </summary>
    public IrcClientWrapper(
        IIrcConfigurationProvider configurationProvider,
        ILogger<IrcClientWrapper> log)
    {
        _config = configurationProvider.IrcConfigurationSection;
        _log = log;
        _innerClient = new StandardIrcClient();
        _innerClient.FloodPreventer = new IrcStandardFloodPreventer(4, 2000);
        _innerClient.Connected += InnerClient_ConnectionComplete;
        _innerClient.ConnectFailed += InnerClient_ConnectionFailed;
        _innerClient.RawMessageReceived += InnerClient_RawMessageReceived;
        _innerClient.ErrorMessageReceived += InnerClient_ErrorMessageReceived;
    }

    #endregion

    #region Event Handlers

    private void InnerClient_RawMessageReceived(object? sender, IrcRawMessageEventArgs e)
    {
        _log.LogTrace("Message Received: {RawMessage}", e.RawContent);

        if (e.Message.Command != "PRIVMSG")
        {
            return;
        }
        
        ChannelMessageReceived?.Invoke(sender, new IrcRawMessageEventArgsWrapper(e));
    }

    private void InnerClient_ConnectionComplete(object? sender, EventArgs e)
    {
        ReadyToJoinChannels?.Invoke(sender, e);
    }

    private void InnerClient_ConnectionFailed(object? sender, IrcErrorEventArgs e)
    {
        ConnectionFailed?.Invoke(sender, new IrcErrorEventArgsWrapper(e));
    }

    private void InnerClient_ErrorMessageReceived(object? sender, IrcErrorMessageEventArgs e)
    {
        ErrorMessageReceived?.Invoke(
            sender,
            new IrcErrorEventArgsWrapper(new IrcErrorEventArgs(new Exception(e.Message))));
    }

    #endregion

    #region Events/Delegates

    /// <inheritdoc />
    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    /// <inheritdoc />
    public event EventHandler? ReadyToJoinChannels;

    /// <inheritdoc />
    public event EventHandler<IIrcErrorEventArgs>? ConnectionFailed; 
    /// <inheritdoc />
    public event EventHandler<IIrcErrorEventArgs>? ErrorMessageReceived; 

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public void Connect()
    {
        // Wait until connection has succeeded or timed out.
        using var connectedEvent = new ManualResetEventSlim(false);
        _innerClient.Connected += (sender2, e2) => connectedEvent.Set();
        _innerClient.Connect(_config.Server, false, new IrcUserRegistrationInfo
        {
            NickName = _config.Nick,
            UserName = _config.User,
            RealName = _config.User + " Text Bot"
        });

        if (!connectedEvent.Wait(10000))
        {
            _innerClient.Dispose();
        }
    }

    /// <inheritdoc />
    public void JoinChannel(string channel)
    {
        _innerClient.Channels.Join(channel);
    }

    /// <inheritdoc />
    public void SendMessage(string message, params string[] targets)
    {
        _innerClient.LocalUser.SendMessage(targets, message);
    }

    #endregion
}