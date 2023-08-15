using System.Diagnostics.CodeAnalysis;
using DammitBot.Events;
using DammitBot.Utilities;
using Microsoft.Extensions.Logging;

namespace DammitBot.Protocols;

/// <inheritdoc />
public class Console : IConsole
{
    #region Constants

    /// <inheritdoc cref="Name" />
    public const string PROTOCOL_NAME = nameof(Console);
    
    #endregion
    
    #region Private Members

    private readonly IConsoleMainLoop _mainLoop;
    private readonly ILogger<Console> _log;
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    public virtual string Name => PROTOCOL_NAME;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="Console"/> class.
    /// </summary>
    public Console(IConsoleMainLoop mainLoop, ILogger<Console> log)
    {
        _mainLoop = mainLoop;
        _log = log;
    }
    
    #endregion
    
    #region Events

    /// <inheritdoc />
    public virtual event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    
    #endregion
    
    #region Private Methods

    private void MainLoop_ChannelMessageReceived(object? sender, MessageEventArgs e)
    {
        _log.LogReceivedMessage(e);
        ChannelMessageReceived?.Invoke(sender, e);
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    /// <remarks>
    /// This implementation wires up event bubbling from the main loop whenever the user types something
    /// and presses enter.
    /// </remarks>
    public virtual void Initialize()
    {
        _log.LogInformation($"Initializing {nameof(Console)} protocol");
        _mainLoop.ChannelMessageReceived += MainLoop_ChannelMessageReceived;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation does nothing.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public virtual void Cleanup() {}
    
    /// <inheritdoc />
    /// <remarks>
    /// This implementation writes the message to the console, as it has no channels to speak of.
    /// </remarks>
    public virtual void SayToAll(string message)
    {
        System.Console.WriteLine($"###TO ALL: {message}");
    }

    /// <inheritdoc />
    /// <inheritdoc cref="SayToAll" path="remarks" />
    public virtual void SayToChannel(string channel, string message)
    {
        System.Console.WriteLine($"###TO '{channel}': {message}");
    }
    
    #endregion
}