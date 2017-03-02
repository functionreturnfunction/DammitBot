using System;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Utilities
{
    public interface IProtocolService : IPluginThingy
    {
        void SayToAll(string message);
        event EventHandler<MessageEventArgs> ChannelMessageReceived;
    }
}