using DammitBot.Events;
using Microsoft.Extensions.Logging;

namespace DammitBot.Utilities;

public class ConsoleMainLoop : MainLoop, IConsoleMainLoop
{
    public ConsoleMainLoop(IBot bot, ILogger<ConsoleMainLoop> log) : base(bot, log) { }

    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    private static void PromptForUserInput()
    {
        System.Console.Write("Message to bot: ");
    }

    private void AcceptUserInput()
    {
        ChannelMessageReceived?.Invoke(
            this,
            new MessageEventArgs(
                System.Console.ReadLine()!,
                Protocols.Console.Console.PROTOCOL_NAME,
                Protocols.Console.Console.PROTOCOL_NAME,
                Protocols.Console.Console.PROTOCOL_NAME + " User"));
    }
    
    protected override void DoPreLoopStuff()
    {
        PromptForUserInput();
    }

    protected override void DoLoopStuff()
    {
        if (!System.Console.KeyAvailable)
        {
            return;
        }
        
        AcceptUserInput();

        // don't want to prompt again if we're shutting down
        if (Bot.Running)
        {
            PromptForUserInput();
        }
    }
}