using System;
using System.Collections.Generic;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Service for providing <see cref="Assembly"/> instances, useful for finding various <see cref="Type"/>s
/// within them.
/// </summary>
public interface IAssemblyService
{
    #region Abstract Properties
    
    /// <summary>
    /// Core assembly in which the <see cref="IBot"/> can be found.
    /// </summary>
    Assembly MainAssembly { get; }
    
    #endregion
    
    #region Abstract Methods

    /// <summary>
    /// Get all available <see cref="Assembly"/> instances.
    /// </summary>
    IEnumerable<Assembly> GetAllAssemblies();
    /// <summary>
    /// Get all available plugin <see cref="Assembly"/> instances.  Plugin assemblies are named
    /// "DammitBot.Plugins.*.dll".
    /// </summary>
    IEnumerable<Assembly> GetPluginAssemblies();

    #endregion
}