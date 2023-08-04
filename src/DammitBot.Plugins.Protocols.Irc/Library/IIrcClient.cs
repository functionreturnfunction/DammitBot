using System;
using DammitBot.Events;

namespace DammitBot.Library;

public interface IIrcClient
{
    event EventHandler? ReadyToJoinChannels;
    event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    void Connect();
    void JoinChannel(string channel);
    void SendMessage(string message, params string[] targets);
}