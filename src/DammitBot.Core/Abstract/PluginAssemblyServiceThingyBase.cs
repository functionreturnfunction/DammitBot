using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;

namespace DammitBot.Abstract
{
    public abstract class PluginAssemblyServiceThingyBase<TThingy>
        where TThingy : IPluginThingy
    {
        #region Private Members

        protected IAssemblyService _assemblyService;
        protected IInstantiationService _instantiationService;
        protected IList<TThingy> _thingies;

        #endregion

        #region Constructors

        public PluginAssemblyServiceThingyBase(
            IAssemblyService assemblyService,
            IInstantiationService instantiationService)
        {
            _assemblyService = assemblyService;
            _instantiationService = instantiationService;
            _thingies = new List<TThingy>();
        }

        #endregion

        #region Exposed Methods

        public void Initialize()
        {
            foreach (
                var type in
                _assemblyService.GetPluginAssemblies()
                    .GetTypes()
                    .Where(t => !t.IsAbstract && typeof(TThingy).IsAssignableFrom(t)))
            {
                var plugin = (TThingy)_instantiationService.GetInstance(type);

                // initialize "Priority" plugins right away
                if (plugin.Priority)
                {
                    plugin.Initialize();
                }

                _thingies.Add(plugin);
            }

            // initialize non-priority plugins afterward
            foreach (var plugin in _thingies.Where(t => !t.Priority))
            {
                plugin.Initialize();
            }
        }

        public void Cleanup()
        {
            foreach (var plugin in _thingies)
            {
                plugin.Cleanup();
            }
        }

        #endregion
    }
}