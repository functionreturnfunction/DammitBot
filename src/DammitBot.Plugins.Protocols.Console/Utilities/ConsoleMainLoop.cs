using DammitBot.Events;
using Microsoft.Extensions.Logging;
using Console = DammitBot.Protocols.Console;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IConsoleMainLoop" />
public class ConsoleMainLoop : MainLoop, IConsoleMainLoop
{
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="ConsoleMainLoop"/> class.
    /// </summary>
    public ConsoleMainLoop(IBot bot, ILogger<ConsoleMainLoop> log) : base(bot, log) { }
    
    #endregion
    
    #region Events

    /// <inheritdoc />
    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    
    #endregion
    
    #region Private Methods

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
                Console.PROTOCOL_NAME,
                Console.PROTOCOL_NAME,
                Console.PROTOCOL_NAME + " User"));
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This implementation will present a prompt to the user asking for input to use as a message.
    /// </remarks>
    protected override void DoPreLoopStuff()
    {
        PromptForUserInput();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation watches for user input, which it will fire off as an event when they press
    /// enter.
    /// </remarks>
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
    
    #endregion
}