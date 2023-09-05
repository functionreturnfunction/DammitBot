using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Microsoft.Extensions.Logging;

namespace DammitBot.Abstract;

/// <inheritdoc cref="ThingyServiceBase{TThingy}"/>
/// <remarks>
/// This implementation operates specifically on types implementing <see cref="IPluginThingy"/>, will
/// <see cref="IPluginThingy.Initialize"/> all instantiated types, and will specifically
/// <see cref="IPluginThingy.Initialize"/> instances which are <see cref="IPluginThingy.Priority"/> before
/// those which aren't. 
/// </remarks>
public abstract class PluginThingyServiceBase<TThingy> : ThingyServiceBase<TThingy>
    where TThingy : IPluginThingy
{
    #region Private Members
    
    private bool _needsCleanup;
    private readonly ILogger<PluginThingyServiceBase<TThingy>> _log;
    
    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="PluginThingyServiceBase{TThingy}"/> class.
    /// </summary>
    public PluginThingyServiceBase(
        IAssemblyTypeService assemblyTypeService,
        IInstantiationService instantiationService,
        ILogger<PluginThingyServiceBase<TThingy>> log)
        : base(assemblyTypeService, instantiationService)
    {
        _log = log;
    }

    #endregion
    
    #region Private Methods

    /// <inheritdoc cref="ThingyServiceBase{TThingy}.IsViable"/>
    /// <remarks>
    /// This implementation looks for types which are interfaces, making instantiation handling simpler
    /// in testing.
    /// </remarks>
    protected override bool IsViable(Type type)
    {
        return type.IsInterface;
    }

    /// <inheritdoc cref="ThingyServiceBase{TThingy}.PostInstantiate"/>
    /// <remarks>
    /// This implementation <see cref="IPluginThingy.Initialize"/>s the supplied
    /// <paramref name="thingies"/>, first initializing the ones which are
    /// <see cref="IPluginThingy.Priority"/>, and then initializing the rest.
    /// </remarks>
    protected override void PostInstantiate(IEnumerable<TThingy> thingies)
    {
        // initialize "Priority" plugins right away
        foreach (var plugin in thingies.Where(t => t.Priority))
        {
            _log.LogDebug("Priority-initializing {Plugin}", plugin.GetType().Name);
            
            plugin.Initialize();
        }

        // initialize non-priority plugins afterward
        foreach (var plugin in thingies.Where(t => !t.Priority))
        {
            _log.LogDebug("Initializing {Plugin}", plugin.GetType().Name);
            
            plugin.Initialize();
        }

        _needsCleanup = true;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IPluginThingy.Initialize"/>
    /// <remarks>
    /// This implementation loops through <see cref="ThingyServiceBase{TThingy}.Thingies"/>.
    /// </remarks>
    public void Initialize()
    {
        foreach (var _ in Thingies) {}
    }
    
    /// <inheritdoc cref="IPluginThingy.Cleanup"/>
    /// <remarks>
    /// This implementation calls <see cref="IPluginThingy.Cleanup"/> on all
    /// <see cref="ThingyServiceBase{TThingy}.Thingies"/>, but only if they've been
    /// <see cref="IPluginThingy.Initialize"/>ed.
    /// </remarks>
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