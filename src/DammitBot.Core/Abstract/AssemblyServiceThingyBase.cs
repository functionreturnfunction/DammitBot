using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.Abstract;

/// <summary>
/// Uses an <see cref="AssemblyService"/> to loop through all concrete types in all available assemblies
/// which inherit from or implement <typeparamref name="TThingy"/>, and instantiates them using an
/// <see cref="IInstantiationService"/>.
///
/// Instances are cached, so they'll persist and be portable along with a given instance of this class.
/// </summary>
public abstract class AssemblyServiceThingyBase<TThingy>
{
    #region Private Members

    private readonly IAssemblyService _assemblyService;
    private readonly IInstantiationService _instantiationService;
    private IEnumerable<TThingy>? _thingies;

    #endregion

    /// <summary>
    /// Instances of all available concrete implementations of <see cref="TThingy"/>.
    /// </summary>
    public IEnumerable<TThingy> Thingies => _thingies ??= GetThingies();

    #region Constructors

    /// <summary>
    /// Constructor for AssemblyServiceThingyBase.
    /// </summary>
    protected AssemblyServiceThingyBase(
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
    {
        _assemblyService = assemblyService;
        _instantiationService = instantiationService;
    }

    #endregion

    #region Private Methods

    protected virtual void PostInstantiate(IEnumerable<TThingy> thingies) { }

    protected virtual bool IsViable(Type type)
    {
        return !type.IsAbstract;
    }

    private TThingy InstantiateThingy(Type thingyType)
    {
        return (TThingy)_instantiationService.GetInstance(thingyType);
    }

    private IEnumerable<TThingy> GetThingies()
    {
        var thingies = _assemblyService.GetPluginAssemblies()
            .GetTypes()
            .Where(t => IsViable(t) && typeof(TThingy).IsAssignableFrom(t))
            .Select(InstantiateThingy)
            .ToList();

        PostInstantiate(thingies);

        return thingies;
    }

    #endregion
}