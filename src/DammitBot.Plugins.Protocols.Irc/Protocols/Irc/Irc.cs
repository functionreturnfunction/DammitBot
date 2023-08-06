using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols.Irc;

public class Irc : IIrc
{
    #region Constants

    public const string PROTOCOL_NAME = "Irc";

    #endregion

    #region Private Members

    private readonly IIrcClientFactory _ircClientFactory;
    private readonly ILogger _log;
    private readonly IIrcConfigurationSection _config;
    private IIrcClient? _irc;

    #endregion

    #region Constructors

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

    public virtual event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    public virtual string Name => PROTOCOL_NAME;

    #endregion
    
    #region Private Methods

    // IrcDotNet doesn't like sending messages with newline chars, so this breaks them up into separate
    // messages
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

    public virtual void Cleanup() {}

    public virtual void SayToChannel(string channel, string message)
    {
        SendMessage(message, channel);
    }

    public virtual void SayToAll(string message)
    {
        SendMessage(message, _config.Channels);
    }

    #endregion
}