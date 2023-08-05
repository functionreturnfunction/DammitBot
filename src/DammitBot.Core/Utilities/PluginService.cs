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
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
        : base(assemblyService, instantiationService) { }
    
    #endregion
}