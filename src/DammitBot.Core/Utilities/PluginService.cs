using DammitBot.Abstract;
using DammitBot.Wrappers;
using Microsoft.Extensions.Logging;

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
        IInstantiationService instantiationService,
        ILogger<PluginService> log)
        : base(assemblyTypeService, instantiationService, log) { }
    
    #endregion
}