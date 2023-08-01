using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.Utilities
{
    public class ProtocolService : PluginAssemblyServiceThingyBase<IProtocol>, IProtocolService
    {
        #region Constructors

        public ProtocolService(
            IAssemblyService assemblyService,
            IInstantiationService instantiationService)
            : base(assemblyService, instantiationService) { }

        #endregion

        #region Events/Delegates

        public event EventHandler<MessageEventArgs> ChannelMessageReceived
        {
            add
            {
                foreach (var protocol in _thingies)
                {
                    protocol.ChannelMessageReceived += value;
                }
            }
            remove
            {
                foreach (var protocol in _thingies)
                {
                    protocol.ChannelMessageReceived -= value;
                }
            }
        }

        #endregion

        #region Exposed Methods

        public void SayToAll(string message)
        {
            foreach (var protocol in _thingies)
            {
                protocol.SayToAll(message);
            }
        }

        #endregion

        public void SayToChannel(string protocol, string channel, string message)
        {
            _thingies.Single(x => x.Name == protocol).SayToChannel(channel, message);
        }
    }
}