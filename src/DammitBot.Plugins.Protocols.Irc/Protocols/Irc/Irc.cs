using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Wrappers;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols.Irc;

public class Irc : IIrc
{
    #region Constants

    public const string PROTOCOL_NAME = "Irc";

    #endregion

    #region Private Members

    protected readonly IIrcClientFactory _ircClientFactory;
    protected readonly ILogger _log;
    protected readonly IIrcConfigurationSection _config;
    protected IIrcClient? _irc;

    #endregion

    #region Constructors

    public Irc(
        IIrcClientFactory ircClientFactory,
        IIrcConfigurationManager configurationManager,
        ILogger<Irc> log)
    {
        _ircClientFactory = ircClientFactory;
        _config = configurationManager.IrcConfigurationSection;
        _log = log;
    }

    #endregion

    #region Event Handlers

    private void Irc_ChannelMessageReceived(object sender, MessageEventArgs e)
    {
        _log.LogDebug("Message received: '{MessageText}'", e.Message);
        ChannelMessageReceived?.Invoke(sender, e);
    }

    private void Irc_ConnectionComplete(object sender, EventArgs e)
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

    #region Exposed Methods

    public virtual void Initialize()
    {
        _log.LogInformation(
            "Initiating client: '{Server}', '{Nick}', '{User}'",
            _config.Server,
            _config.Nick,
            _config.User);
        
        _irc = _ircClientFactory.Build(_config);
        _irc.ConnectionComplete += Irc_ConnectionComplete;
        _irc.ChannelMessageReceived += Irc_ChannelMessageReceived;
        _irc.ConnectAsync();
    }

    public virtual void Cleanup() {}

    public virtual void SayToChannel(string channel, string message)
    {
        if (_irc == null)
        {
            throw new InvalidOperationException(
                $"An {nameof(Irc)} instance cannot be used before {nameof(Initialize)} has " +
                "been called on it");
        }
            
        _irc.SendMessage(message, channel);
    }

    public virtual void SayToAll(string message)
    {
        if (_irc == null)
        {
            throw new InvalidOperationException(
                $"An {nameof(Irc)} instance cannot be used before {nameof(Initialize)} has " +
                "been called on it");
        }
            
        _irc.SendMessage(message, _config.Channels);
    }

    #endregion
}