using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;

namespace DammitBot.Library;

public class TestAssemblyService : AssemblyService
{
    private readonly bool _includeActualPlugins;

    public TestAssemblyService(bool includeActualPlugins = true)
    {
        _includeActualPlugins = includeActualPlugins;
    }

    protected override IEnumerable<string> FindPluginDllPaths()
    {
        var testDll =
            new[] { GetType().Assembly.GetName().Name + ".dll" }; 
        return _includeActualPlugins
            ? base.FindPluginDllPaths().Concat(testDll)
            : testDll;
    }
}