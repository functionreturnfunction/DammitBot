using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Library;

namespace DammitBot.Utilities;

/// <inheritdoc />
/// <remarks>
/// This implementation prompts the user for input and then reads that input, passing it along as a
/// <see cref="ChannelMessageReceived"/> event when they press enter.
/// </remarks>
public interface IConsoleMainLoop : IMainLoop
{
    #region Events
    
    /// <summary>
    /// Event fired when the user inputs text into the console and presses enter. 
    /// </summary>
    event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    
    #endregion
    
    #region Abstract Methods

    /// <inheritdoc cref="IConsoleIO.WriteLine" />
    void WriteLine(string value);

    #endregion
}