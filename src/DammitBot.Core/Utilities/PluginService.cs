using System.Collections.Generic;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;

namespace DammitBot.Utilities
{
    public class PluginService : IPluginService
    {
        private readonly IAssemblyService _assemblyService;
        private readonly IInstantiationService _instantiationService;
        private readonly IList<IPlugin> _plugins;

        public PluginService(IAssemblyService assemblyService, IInstantiationService instantiationService)
        {
            _assemblyService = assemblyService;
            _instantiationService = instantiationService;
            _plugins = new List<IPlugin>();
        }

        public void Initialize()
        {
            foreach (
                var type in
                _assemblyService.GetPluginAssemblies()
                    .GetTypes()
                    .Where(t => !t.IsAbstract && typeof(IPlugin).IsAssignableFrom(t)))
            {
                var plugin = (IPlugin)_instantiationService.GetInstance(type);
                plugin.Initialize();
                _plugins.Add(plugin);
            }
        }

        public void Cleanup()
        {
            foreach (var plugin in _plugins)
            {
                plugin.Cleanup();
            }
        }
    }
}