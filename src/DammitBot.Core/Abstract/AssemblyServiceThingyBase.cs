using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;

namespace DammitBot.Abstract
{
    public abstract class AssemblyServiceThingyBase<TThingy>
    {
        #region Private Members

        private IAssemblyService _assemblyService;
        private IInstantiationService _instantiationService;
        private IEnumerable<TThingy> _thingies;

        #endregion

        public IEnumerable<TThingy> Thingies => _thingies ?? (_thingies = GetThingies());

        #region Constructors

        public AssemblyServiceThingyBase(IAssemblyService assemblyService, IInstantiationService instantiationService)
        {
            _assemblyService = assemblyService;
            _instantiationService = instantiationService;
        }

        #endregion

        #region Private Methods

        protected virtual TThingy InstantiateThingy(Type thingyType)
        {
            return (TThingy)_instantiationService.GetInstance(thingyType);
        }

        protected virtual IEnumerable<TThingy> GetThingies()
        {
            foreach (
                var type in
                _assemblyService.GetPluginAssemblies()
                    .GetTypes()
                    .Where(t => !t.IsAbstract && typeof(TThingy).IsAssignableFrom(t)))
            {
                yield return InstantiateThingy(type);
            }
        }

        #endregion
    }
}