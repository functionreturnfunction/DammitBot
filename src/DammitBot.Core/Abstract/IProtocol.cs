using System;
using DammitBot.Events;

namespace DammitBot.Abstract
{
    public interface IProtocol : IPluginThingy
    {
        void SayToAll(string message);

        event EventHandler<MessageEventArgs> ChannelMessageReceived;
    }
}