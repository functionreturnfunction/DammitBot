using System;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Protocols.Irc.Configuration;
using DammitBot.Protocols.Irc.Wrappers;
using log4net;

namespace DammitBot.Protocols.Irc
{
    public class Irc : IProtocol
    {
        public const string PROTOCOL_NAME = "Irc";

        private readonly IIrcClientFactory _ircClientFactory;
        private readonly ILog _log;
        private readonly IrcConfigurationSection _config;
        private IIrcClient _irc;

        public Irc(IIrcClientFactory ircClientFactory, IIrcConfigurationManager configurationManager, ILog log)
        {
            _ircClientFactory = ircClientFactory;
            _config = configurationManager.IrcConfigurationSection;
            _log = log;
        }

        public void Initialize()
        {
            _log.Info($"Initiating client: '{_config.Server}', '{_config.Nick}', '{_config.User}'");
            _irc = _ircClientFactory.Build(_config);
            _irc.ConnectionComplete += Irc_ConnectionComplete;
            _irc.ChannelMessageRecieved += Irc_ChannelMessageReceived;
            _irc.ConnectAsync();
        }

        private void Irc_ChannelMessageReceived(object sender, MessageEventArgs e)
        {
            _log.Debug($"Message received: {e.Message}");
            ChannelMessageReceived.Invoke(sender, e);
        }

        private void Irc_ConnectionComplete(object sender, EventArgs e)
        {
            _log.Info($"Initial connection complete, joining channels '{_config.Channels}'");
            foreach (var channel in _config.Channels.Split(','))
            {
                _irc.JoinChannel(channel);
            }
        }

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }

        public void SayToAll(string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<MessageEventArgs> ChannelMessageReceived;
    }
}