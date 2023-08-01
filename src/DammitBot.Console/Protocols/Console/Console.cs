using System;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Console.Protocols.Console;

public class Console : IProtocol
{
    public const string PROTOCOL_NAME = nameof(Console);
    
    public void Initialize() {}

    public void Cleanup() {}

    public string Name => PROTOCOL_NAME;
    
    public void SayToAll(string message)
    {
        System.Console.WriteLine($"###TO ALL: {message}");
    }

    public void SayToChannel(string channel, string message)
    {
        System.Console.WriteLine($"###TO '{channel}': {message}");
    }

    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;
}