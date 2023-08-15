namespace DammitBot.Library;

/// <summary>
/// Abstraction for handling input/output from/to the commandline.
/// </summary>
public interface IConsoleIO
{
    #region Abstract Properties
    
    /// <inheritdoc cref="System.Console.KeyAvailable"/>
    bool KeyAvailable { get; }
    
    #endregion
    
    #region Abstract Methods
    
    /// <inheritdoc cref="System.Console.WriteLine(string)"/>
    void WriteLine(string value);

    /// <inheritdoc cref="System.Console.Write(string)"/>
    void Write(string value);

    /// <inheritdoc cref="System.Console.ReadLine"/>
    string ReadLine();
    
    #endregion
}