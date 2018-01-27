using System;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;

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

        #region Private Methods

        private void Protocols_ChannelMessageReceived(object sender, MessageEventArgs e)
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

            _pluginService.Initialize();
            _protocolService.Initialize();
            _protocolService.ChannelMessageReceived += Protocols_ChannelMessageReceived;

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

        public void ReplyToMessage(MessageEventArgs args, string response)
        {
            _protocolService.SayToChannel(args.Protocol, args.Channel, response);
        }

        public void Dispose()
        {
            _pluginService.Cleanup();
            _protocolService.ChannelMessageReceived -= Protocols_ChannelMessageReceived;
        }

        #endregion
    }
}
