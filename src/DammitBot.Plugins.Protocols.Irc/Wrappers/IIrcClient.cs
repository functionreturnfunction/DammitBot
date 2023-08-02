using System;
using DammitBot.Events;

namespace DammitBot.Wrappers;

public interface IIrcClient
{
    event EventHandler? ConnectionComplete;
    event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    void ConnectAsync();
    void JoinChannel(string channel);
    void SendMessage(string message, params string[] targets);
}