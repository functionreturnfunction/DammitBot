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

        public void RegisterChannelMessageReceivedHandler(EventHandler<MessageEventArgs> fn)
        {
            foreach (var protocol in _thingies)
            {
                protocol.ChannelMessageReceived += fn;
            }
        }

        public void UnregisterChannelMessageReceivedHandler(EventHandler<MessageEventArgs> fn)
        {
            foreach (var protocol in _thingies)
            {
                protocol.ChannelMessageReceived -= fn;
            }
        }
    }
}