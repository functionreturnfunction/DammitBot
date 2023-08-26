using System.Collections.Generic;
using System.Linq;
using DammitBot.Configuration;
using DammitBot.Utilities;

namespace DammitBot.Library;

public class TestAssemblyTypeService : AssemblyTypeService
{
    private readonly bool _includeActualPlugins;

    public TestAssemblyTypeService(bool includeActualPlugins = true)
        : base(new MockOptions<BotConfiguration>().Object)
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