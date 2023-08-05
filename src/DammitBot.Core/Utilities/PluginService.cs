using DammitBot.Abstract;
using DammitBot.Wrappers;

namespace DammitBot.Utilities;

public class PluginService : PluginThingyServiceBase<IPlugin>, IPluginService
{
    public PluginService(
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
        : base(assemblyService, instantiationService) { }
}