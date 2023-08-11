using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IAssemblyTypeService"/>
public class AssemblyTypeService : IAssemblyTypeService
{
    #region Private Members

    private Assembly? _mainAssembly;
    private IEnumerable<Assembly>? _pluginAssemblies;

    #endregion

    #region Properties

    private Assembly MainAssembly => _mainAssembly ??= typeof(IBot).Assembly;

    #endregion

    #region Private Methods

    /// <summary>
    /// Find the paths of plugin dlls. 
    /// </summary>
    protected virtual IEnumerable<string> FindPluginDllPaths()
    {
        return Directory.GetFileSystemEntries(
            MainAssembly.GetDirectory(),
            "DammitBot.Plugins.*.dll");
    }

    private IEnumerable<Assembly> InnerGetPluginAssemblies()
    {
        var dlls = FindPluginDllPaths();

        foreach (var dll in dlls)
        {
            yield return LoadAssembly(dll);
        }
    }

    private Assembly LoadAssembly(string dll)
    {
        return Assembly.LoadFrom(dll);
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IAssemblyTypeService.GetTypesFromAllAssemblies"/>
    public IEnumerable<Type> GetTypesFromAllAssemblies()
    {
        return MainAssembly.GetTypes().Concat(GetTypesFromPluginAssemblies());
    }

    /// <inheritdoc cref="IAssemblyTypeService.GetTypesFromPluginAssemblies"/>
    public IEnumerable<Type> GetTypesFromPluginAssemblies()
    {
        return (_pluginAssemblies ??= InnerGetPluginAssemblies().ToList())
            .SelectMany(assembly => assembly.GetTypes());
    }

    #endregion
}