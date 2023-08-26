using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DammitBot.Configuration;
using Microsoft.Extensions.FileSystemGlobbing;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IAssemblyTypeService"/>
public class AssemblyTypeService : IAssemblyTypeService
{
    #region Private Members

    private Assembly? _mainAssembly;
    private IEnumerable<Assembly>? _pluginAssemblies;
    private readonly Matcher? _ignoreDllMatcher;

    #endregion

    #region Properties

    private Assembly MainAssembly => _mainAssembly ??= typeof(IBot).Assembly;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="AssemblyTypeService"/> class.
    /// </summary>
    public AssemblyTypeService(BotConfiguration botConfiguration)
    {
        if (botConfiguration.IgnoreAssemblies != null)
        {
            _ignoreDllMatcher = new();
            _ignoreDllMatcher.AddIncludePatterns(botConfiguration.IgnoreAssemblies);
        }
    }
    
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

    private bool ShouldIgnore(string dllPath)
    {
        return _ignoreDllMatcher?.Match(dllPath).HasMatches ?? false;
    }

    private IEnumerable<Assembly> InnerGetPluginAssemblies()
    {
        var dlls = FindPluginDllPaths();

        foreach (var dll in dlls.Where(d => !ShouldIgnore(d)))
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