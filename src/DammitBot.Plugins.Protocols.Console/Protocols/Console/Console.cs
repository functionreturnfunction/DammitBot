using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Protocols.Console;

public class Console : IProtocol
{
    public const string PROTOCOL_NAME = nameof(Console);
    
    public virtual void Initialize() {}

    public virtual void Cleanup() {}

    public virtual string Name => PROTOCOL_NAME;
    
    public virtual void SayToAll(string message)
    {
        System.Console.WriteLine($"###TO ALL: {message}");
    }

    public virtual void SayToChannel(string channel, string message)
    {
        System.Console.WriteLine($"###TO '{channel}': {message}");
    }

    public virtual event EventHandler<MessageEventArgs>? ChannelMessageReceived;
}