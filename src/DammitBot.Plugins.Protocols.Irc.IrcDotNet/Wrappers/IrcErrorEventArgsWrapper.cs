using System;
using DammitBot.Library;
using IrcDotNet;

namespace DammitBot.Wrappers;

/// <inheritdoc />
/// <remarks>
/// This implementation serves as a wrapper/adapter for IrcDotNet's <see cref="IrcErrorEventArgs"/> class.
/// </remarks>
public class IrcErrorEventArgsWrapper : IIrcErrorEventArgs
{
    #region Private Members
    
    private readonly IrcErrorEventArgs _innerEventArgs;
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    public Exception Exception => _innerEventArgs.Error;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcErrorEventArgsWrapper"/> class.
    /// </summary>
    public IrcErrorEventArgsWrapper(IrcErrorEventArgs innerEventArgs)
    {
        _innerEventArgs = innerEventArgs;
    }
    
    #endregion
}