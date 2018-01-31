using System;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Protocols.Irc.Wrappers;
using log4net;

namespace DammitBot.Protocols.Irc
{
    public class Irc : IProtocol
    {
        #region Constants

        public const string PROTOCOL_NAME = "Irc";

        #endregion

        #region Private Members

        protected readonly IIrcClientFactory _ircClientFactory;
        protected readonly ILog _log;
        protected readonly IIrcConfigurationSection _config;
        protected IIrcClient _irc;

        #endregion

        #region Constructors

        /// <summary>
        /// For testing purposes only!!
        /// </summary>
        public Irc() { }

        public Irc(IIrcClientFactory ircClientFactory, IIrcConfigurationManager configurationManager, ILog log)
        {
            _ircClientFactory = ircClientFactory;
            _config = configurationManager.IrcConfigurationSection;
            _log = log;
        }

        #endregion

        #region Event Handlers

        private void Irc_ChannelMessageReceived(object sender, MessageEventArgs e)
        {
            _log.Debug($"Message received: {e.Message}");
            ChannelMessageReceived?.Invoke(sender, e);
        }

        private void Irc_ConnectionComplete(object sender, EventArgs e)
        {
            _log.Info($"Initial connection complete, joining channels '{string.Join(",", _config.Channels)}'");
            foreach (var channel in _config.Channels)
            {
                _irc.JoinChannel(channel);
            }
        }

        #endregion

        #region Events/Delegates

        public virtual event EventHandler<MessageEventArgs> ChannelMessageReceived;

        public virtual string Name => PROTOCOL_NAME;

        public virtual void SayToChannel(string channel, string message)
        {
            _irc.SendMessage(message, channel);
        }

        #endregion

        #region Exposed Methods

        public virtual void Initialize()
        {
            _log.Info($"Initiating client: '{_config.Server}', '{_config.Nick}', '{_config.User}'");
            _irc = _ircClientFactory.Build(_config);
            _irc.ConnectionComplete += Irc_ConnectionComplete;
            _irc.ChannelMessageRecieved += Irc_ChannelMessageReceived;
            _irc.ConnectAsync();
        }

        public virtual void Cleanup() {}

        public virtual void SayToAll(string message)
        {
            _irc.SendMessage(message, _config.Channels);
        }

        #endregion
    }
}