using DammitBot.Events;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols.Console;

public class Console : IConsole
{
    public const string PROTOCOL_NAME = nameof(Console);

    private readonly IConsoleMainLoop _mainLoop;
    private readonly ILogger<Console> _log;

    public Console(IConsoleMainLoop mainLoop, ILogger<Console> log)
    {
        _mainLoop = mainLoop;
        _log = log;
    }

    public virtual event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    public virtual void Initialize()
    {
        _log.LogInformation($"Initializing {nameof(Console)} protocol");
        _mainLoop.ChannelMessageReceived += MainLoop_ChannelMessageReceived;
    }

    private void MainLoop_ChannelMessageReceived(object? sender, MessageEventArgs e)
    {
        _log.LogReceivedMessage(e);
        ChannelMessageReceived?.Invoke(sender, e);
    }

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
}