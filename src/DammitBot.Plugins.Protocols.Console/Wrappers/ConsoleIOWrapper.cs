using System.Diagnostics.CodeAnalysis;
using DammitBot.Library;

namespace DammitBot.Wrappers;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public class ConsoleIOWrapper : IConsoleIO
{
    #region Properties

    /// <inheritdoc />
    public bool KeyAvailable => Console.KeyAvailable;
    
    #endregion
    
    #region Abstract Methods

    /// <inheritdoc />
    public void WriteLine(string value) => Console.WriteLine(value);

    /// <inheritdoc />
    public void Write(string value) => Console.Write(value);

    /// <inheritdoc />
    public string ReadLine() => Console.ReadLine()!;
    
    #endregion
}