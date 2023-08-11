using System;
using System.Collections.Generic;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Service for providing <see cref="Type"/>s from available assemblies, including plugin assemblies.
/// </summary>
public interface IAssemblyTypeService
{
    #region Abstract Methods

    /// <summary>
    /// Get all types from all available <see cref="Assembly"/> instances, including DammitBot.Core.
    /// </summary>
    IEnumerable<Type> GetTypesFromAllAssemblies();
    /// <summary>
    /// Get all types from all available plugin <see cref="Assembly"/> instances.  Plugin assemblies are
    /// named "DammitBot.Plugins.*.dll".
    /// </summary>
    IEnumerable<Type> GetTypesFromPluginAssemblies();

    #endregion
}