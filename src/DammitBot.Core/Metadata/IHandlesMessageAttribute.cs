using System.Text.RegularExpressions;
using DammitBot.Abstract;

namespace DammitBot.Metadata;

/// <summary>
/// Attribute applied to <see cref="IMessageHandler{MessageEventArgs}"/> implementations which provides a
/// <see cref="Regex"/> to compare against received messages and determine which handler(s) would be
/// appropriate. 
/// </summary>
public interface IHandlesMessageAttribute
{
    /// <summary>
    /// <see cref="Regex"/> to compare received messages against when locating appropriate
    /// <see cref="IMessageHandler{MessageEventArgs}"/> implementations.
    /// </summary>
    Regex Regex { get; }
    
    /// <summary>
    /// Boolean value indicating whether or not a message should only be acted upon if the user who sent
    /// it is an admin.
    /// </summary>
    bool AdminOnly { get; }
}