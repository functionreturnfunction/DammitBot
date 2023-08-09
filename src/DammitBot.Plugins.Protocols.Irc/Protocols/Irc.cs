﻿using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols;

/// <inheritdoc />
public class Irc : IIrc
{
    #region Constants

    /// <inheritdoc cref="Name" />
    public const string PROTOCOL_NAME = "Irc";

    #endregion

    #region Private Members

    private readonly IIrcClientFactory _ircClientFactory;
    private readonly ILogger _log;
    private readonly IIrcConfigurationSection _config;
    private IIrcClient? _irc;

    #endregion

    #region Properties

    /// <inheritdoc />
    public virtual string Name => PROTOCOL_NAME;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="Irc"/> class.
    /// </summary>
    public Irc(
        IIrcClientFactory ircClientFactory,
        IIrcConfigurationProvider configurationProvider,
        ILogger<Irc> log)
    {
        _ircClientFactory = ircClientFactory;
        _config = configurationProvider.IrcConfigurationSection;
        _log = log;
    }

    #endregion

    #region Event Handlers

    private void Irc_ChannelMessageReceived(object? sender, MessageEventArgs e)
    {
        _log.LogReceivedMessage(e);
        ChannelMessageReceived?.Invoke(sender, e);
    }

    private void Irc_ConnectionComplete(object? sender, EventArgs e)
    {
        _log.LogInformation(
            "Initial connection complete, joining channels '{Channels}'",
            string.Join(",", _config.Channels));
        
        foreach (var channel in _config.Channels)
        {
            // in theory this cannot be null here, because this is an event handler assigned to the
            // ConnectionComplete event of _irc 
            _irc!.JoinChannel(channel);
        }
    }

    #endregion

    #region Events/Delegates

    /// <inheritdoc />
    public virtual event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    #endregion
    
    #region Private Methods

    /// <summary>
    /// IrcDotNet doesn't like sending messages with newline chars, so this breaks them up into separate
    /// messages
    /// </summary>
    private void SendMessage(string message, params string[] targets)
    {
        if (_irc == null)
        {
            throw new InvalidOperationException(
                $"An {nameof(Irc)} instance cannot be used before {nameof(Initialize)} has " +
                "been called on it");
        }
            
        foreach (var chunk in message.Split(Environment.NewLine))
        {
            _irc.SendMessage(chunk, targets);
        }
    }
    
    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    /// <remarks>
    /// This implementation wires up some event handling onto the <see cref="IIrcClient"/>, and then uses
    /// it to connect to the configured server.
    /// </remarks>
    public virtual void Initialize()
    {
        _log.LogInformation(
            "Initiating client: '{Server}', '{Nick}', '{User}'",
            _config.Server,
            _config.Nick,
            _config.User);
        
        _irc = _ircClientFactory.Build();
        _irc.ReadyToJoinChannels += Irc_ConnectionComplete;
        _irc.ChannelMessageReceived += Irc_ChannelMessageReceived;
        _irc.Connect();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation does nothing.
    /// </remarks>
    public virtual void Cleanup() {}

    /// <inheritdoc />
    public virtual void SayToChannel(string channel, string message)
    {
        SendMessage(message, channel);
    }

    /// <inheritdoc />
    public virtual void SayToAll(string message)
    {
        SendMessage(message, _config.Channels);
    }

    #endregion
}