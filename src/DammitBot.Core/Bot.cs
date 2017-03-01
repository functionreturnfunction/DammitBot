using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using log4net;

namespace DammitBot
{
    public class Bot : IBot
    {
        #region Constants

        public const string DEFAULT_GOES_BY = "(?:dammit )?bot";

        #endregion

        #region Private Members

        private readonly BotConfigurationSection _config;
        private readonly IMessageHandlerFactory _handlerFactory;
        private readonly IIrcClientFactory _ircClientFactory;
        private readonly IPluginService _pluginService;
        private IIrcClient _irc;
        private readonly ILog _log;

        #endregion

        #region Properties

        public bool Running { get; private set; }

        #endregion

        #region Constructors

        public Bot(IConfigurationManager configurationManager, IMessageHandlerFactory handlerFactory, IIrcClientFactory ircClientFactory, IPluginService pluginService, ILog log)
        {
            _config = configurationManager.BotConfig;
            _handlerFactory = handlerFactory;
            _ircClientFactory = ircClientFactory;
            _pluginService = pluginService;
            _log = log;
        }

        #endregion

        #region Private Methods

        private void Connect()
        {
            _log.Info($"Initiating client: '{_config.Server}', '{_config.Nick}', '{_config.User}'");
            _irc = _ircClientFactory.Build(_config);
            _irc.ConnectionComplete += Irc_ConnectionComplete;
            _irc.ChannelMessageRecieved += Irc_ChannelMessageReceived;
            _irc.ConnectAsync();
        }

        #endregion

        #region Event Handlers

        private void Irc_ChannelMessageReceived(object sender, MessageEventArgs e)
        {
            _log.Debug($"Message received: {e.IrcMessage.RawMessage}");
            _handlerFactory.BuildHandler(e).Handle(e);
        }

        private void Irc_ConnectionComplete(object sender, EventArgs e)
        {
            _log.Info($"Initial connection complete, joining channel '{_config.Channel}'");
            _irc.JoinChannel(_config.Channel);
        }

        #endregion

        #region Exposed Methods

        public void Run()
        {
            if (Running)
            {
                throw new InvalidOperationException("Bot is already running.");
            }

            _pluginService.Initialize();

            Connect();
            Running = true;
        }

        public void SayInChannel(string message)
        {
            _irc.SendMessage(message, _config.Channel);
        }

        public void Die()
        {
            Running = false;
        }

        public void Dispose()
        {
            _pluginService.Cleanup();
            _irc.ConnectionComplete -= Irc_ConnectionComplete;
            _irc.ChannelMessageRecieved -= Irc_ChannelMessageReceived;
        }

        #endregion
    }
}
