﻿using System;
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

        private readonly IMessageHandlerFactory _handlerFactory;
        private readonly IPluginService _pluginService;
        private readonly IProtocolService _protocolService;

        #endregion

        #region Properties

        public bool Running { get; private set; }

        #endregion

        #region Constructors

        public Bot(IMessageHandlerFactory handlerFactory, IProtocolService protocolService, IPluginService pluginService)
        {
            _handlerFactory = handlerFactory;
            _protocolService = protocolService;
            _pluginService = pluginService;
        }

        #endregion

        #region Event Handlers

        private void Irc_ChannelMessageReceived(object sender, MessageEventArgs e)
        {
            _handlerFactory.BuildHandler(e).Handle(e);
        }

        #endregion

        #region Exposed Methods

        public void Run()
        {
            if (Running)
            {
                throw new InvalidOperationException("Bot is already running.");
            }

            _protocolService.RegisterChannelMessageReceivedHandler(Irc_ChannelMessageReceived);

            _pluginService.Initialize();
            _protocolService.Initialize();

            Running = true;
        }

        public void SayToAll(string message)
        {
            _protocolService.SayToAll(message);
        }

        public void Die()
        {
            Running = false;
        }

        public void Dispose()
        {
            _pluginService.Cleanup();
            _protocolService.UnregisterChannelMessageReceivedHandler(Irc_ChannelMessageReceived);
        }

        #endregion
    }
}
