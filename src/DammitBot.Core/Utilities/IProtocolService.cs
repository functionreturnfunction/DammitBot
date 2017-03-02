using System;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Utilities
{
    public interface IProtocolService : IPluginThingy
    {
        void SayToAll(string message);
        void RegisterChannelMessageReceivedHandler(EventHandler<MessageEventArgs> fn);
        void UnregisterChannelMessageReceivedHandler(EventHandler<MessageEventArgs> fn);
    }
}