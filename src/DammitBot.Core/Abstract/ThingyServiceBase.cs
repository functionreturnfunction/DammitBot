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
public abstract class ThingyServiceBase<TThingy>
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
    protected ThingyServiceBase(
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
    {
        _assemblyService = assemblyService;
        _instantiationService = instantiationService;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Perform any necessary tasks with the instantiated <paramref name="thingies"/> right after they've
    /// been instantiated. 
    /// </summary>
    /// <remarks>This implementation does nothing.</remarks>
    protected virtual void PostInstantiate(IEnumerable<TThingy> thingies) { }

    /// <summary>
    /// Filter to decide whether or not a given type is useful for the purpose intended (besides its type
    /// being assignable to <typeparamref name="TThingy"/>.
    /// </summary>
    /// <remarks>
    /// This implementation looks for types which are not abstract, so only concrete classes are
    /// instantiated (rather than their base type).
    /// </remarks>
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