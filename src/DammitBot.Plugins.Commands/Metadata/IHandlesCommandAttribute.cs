using System.Text.RegularExpressions;
using DammitBot.CommandHandlers;

namespace DammitBot.Metadata;

/// <summary>
/// Attribute applied to <see cref="ICommandHandler"/> implementations which provides a
/// <see cref="Regex"/> to compare against received messages and determine which handler(s) would be
/// appropriate. 
/// </summary>
public interface IHandlesCommandAttribute : IHandlesMessageAttribute
{
    #region Abstract Properties
    
    /// <summary>
    /// Description of the command which is handled by the <see cref="ICommandHandler"/> the attribute is
    /// applied to.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Boolean value indicating whether or not the command being handled should only be accessible to
    /// users who are admins. 
    /// </summary>
    bool AdminOnly { get; }
    
    #endregion
}