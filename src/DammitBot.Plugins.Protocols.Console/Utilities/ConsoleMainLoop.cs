using DammitBot.Events;
using DammitBot.Library;
using Microsoft.Extensions.Logging;
using Console = DammitBot.Protocols.Console;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IConsoleMainLoop" />
public class ConsoleMainLoop : MainLoop, IConsoleMainLoop
{
    #region Constants

    /// <summary>
    /// Prompt message presented to user for input.
    /// </summary>
    public const string USER_PROMPT = "Message to bot: ";
    
    #endregion
    
    #region Private Members

    private readonly IConsoleIO _console;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="ConsoleMainLoop"/> class.
    /// </summary>
    public ConsoleMainLoop(IBot bot, ILogger<ConsoleMainLoop> log, IConsoleIO console) : base(bot, log)
    {
        _console = console;
    }
    
    #endregion
    
    #region Events

    /// <inheritdoc />
    public event EventHandler<MessageEventArgs>? ChannelMessageReceived;

    #endregion
    
    #region Private Methods

    private void PromptForUserInput()
    {
        _console.Write(USER_PROMPT);
    }

    private void AcceptUserInput()
    {
        ChannelMessageReceived?.Invoke(
            this,
            new MessageEventArgs(
                _console.ReadLine()!,
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
        if (!_console.KeyAvailable)
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
    
    #region Exposed Methods

    /// <inheritdoc />
    public void WriteLine(string value)
    {
        _console.WriteLine(value);
    }
    
    #endregion
}