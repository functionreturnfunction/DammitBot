using System;
using DammitBot.Events;

namespace DammitBot.Protocols.Irc.Wrappers
{
    public interface IIrcClient
    {
        event EventHandler ConnectionComplete;
        event EventHandler<MessageEventArgs> ChannelMessageRecieved;
        void ConnectAsync();
        void JoinChannel(string channel);
        void SendMessage(string message, params string[] targets);
    }
}