using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.Abstract;

public abstract class PluginAssemblyServiceThingyBase<TThingy> : AssemblyServiceThingyBase<TThingy>
    where TThingy : IPluginThingy
{
    #region Private Members
    
    private bool _needsCleanup;
    
    #endregion

    #region Constructors

    public PluginAssemblyServiceThingyBase(
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
        : base(assemblyService, instantiationService) { }

    #endregion
    
    #region Private Methods

    protected override bool IsViable(Type type)
    {
        return type.IsInterface;
    }

    protected override void PostInstantiate(IEnumerable<TThingy> thingies)
    {
        // initialize "Priority" plugins right away
        foreach (var plugin in thingies.Where(t => t.Priority))
        {
            plugin.Initialize();
        }

        // initialize non-priority plugins afterward
        foreach (var plugin in thingies.Where(t => !t.Priority))
        {
            plugin.Initialize();
        }

        _needsCleanup = true;
    }

    #endregion

    #region Exposed Methods

    public void Initialize() { }

    public void Cleanup()
    {
        // no sense in cleaning up if nothing's ever been initialized
        if (!_needsCleanup)
        {
            return;
        }
        
        foreach (var plugin in Thingies)
        {
            plugin.Cleanup();
        }
    }

    #endregion
}