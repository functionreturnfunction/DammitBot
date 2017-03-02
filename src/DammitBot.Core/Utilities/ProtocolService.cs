using System;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Wrappers;

namespace DammitBot.Utilities
{
    public class ProtocolService : PluginAssemblyServiceThingyBase<IProtocol>, IProtocolService
    {
        public ProtocolService(IAssemblyService assemblyService, IInstantiationService instantiationService)
            : base(assemblyService, instantiationService)
        {
        }

        public void SayToAll(string message)
        {
            foreach (var protocol in _thingies)
            {
                protocol.SayToAll(message);
            }
        }

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
    }
}