using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="IEnumerable{Assembly}"/> interface.
/// </summary>
public static class AssemblyEnumerableExtensions
{
    #region Exposed Methods

    /// <summary>
    /// Get and return all <see cref="Type"/>s from the assemblies.
    /// </summary>
    public static IEnumerable<Type> GetTypes(this IEnumerable<Assembly> that)
    {
        return that.SelectMany(assembly => assembly.GetTypes());
    }

    #endregion
}