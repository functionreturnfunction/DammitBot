using System;

namespace DammitBot.Library;

/// <summary>
/// <see cref="EventArgs"/> used when an <see cref="IIrcClient"/> encounters an error or exception.
/// </summary>
public interface IIrcErrorEventArgs
{
    #region Abstract Properties
    
    /// <summary>
    /// Exception encountered.
    /// </summary>
    Exception Exception { get; }
    
    #endregion
}