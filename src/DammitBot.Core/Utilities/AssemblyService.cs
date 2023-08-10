using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IAssemblyService"/>
public class AssemblyService : IAssemblyService
{
    #region Private Members

    private Assembly? _mainAssembly;
    private IEnumerable<Assembly>? _pluginAssemblies;

    #endregion

    #region Properties

    /// <inheritdoc cref="IAssemblyService.MainAssembly"/>
    public Assembly MainAssembly => _mainAssembly ??= typeof(IBot).Assembly;

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

    /// <inheritdoc cref="IAssemblyService.GetAllAssemblies"/>
    public IEnumerable<Assembly> GetAllAssemblies()
    {
        yield return MainAssembly;

        foreach (var assembly in GetPluginAssemblies())
        {
            yield return assembly;
        }
    }

    /// <inheritdoc cref="IAssemblyService.GetPluginAssemblies"/>
    public IEnumerable<Assembly> GetPluginAssemblies()
    {
        return _pluginAssemblies ??= InnerGetPluginAssemblies().ToList();
    }

    #endregion
}