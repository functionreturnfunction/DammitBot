using System.Collections.Generic;
using DammitBot.Abstract;

namespace DammitBot.Utilities;

/// <summary>
/// Service providing instances of <see cref="IPlugin"/>.
/// </summary>
public interface IPluginService : IPluginThingy
{
    /// <summary>
    /// Instances of all available concrete implementations of <see cref="IPlugin"/>.
    /// </summary>
    IEnumerable<IPlugin> Thingies { get; }
}