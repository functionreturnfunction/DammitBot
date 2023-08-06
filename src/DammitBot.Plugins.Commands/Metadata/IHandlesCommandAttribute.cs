using System.Text.RegularExpressions;
using DammitBot.CommandHandlers;

namespace DammitBot.Metadata;

/// <summary>
/// Attribute applied to <see cref="ICommandHandler"/> implementations which provides a
/// <see cref="Regex"/> to compare against received messages and determine which handler(s) would be
/// appropriate. 
/// </summary>
public interface IHandlesCommandAttribute : IHandlesMessageAttribute {}