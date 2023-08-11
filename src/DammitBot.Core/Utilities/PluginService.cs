using DammitBot.Abstract;
using DammitBot.Wrappers;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IPluginService"/>
public class PluginService : PluginThingyServiceBase<IPlugin>, IPluginService
{
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="PluginService"/> class.
    /// </summary>
    public PluginService(
        IAssemblyTypeService assemblyTypeService,
        IInstantiationService instantiationService)
        : base(assemblyTypeService, instantiationService) { }
    
    #endregion
}