using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;
using FluentNHibernate.Cfg;

namespace DammitBot.Data.NHibernate.Library
{
    public class MappingConfigurationService : IMappingConfigurationService
    {
        #region Private Members

        protected readonly IAssemblyService _assemblyService;
        protected readonly IInstantiationService _instantiationService;

        #endregion

        #region Constructors

        public MappingConfigurationService(IAssemblyService assemblyService, IInstantiationService instantiationService)
        {
            _assemblyService = assemblyService;
            _instantiationService = instantiationService;
        }

        #endregion

        #region Private Methods

        private IEnumerable<IMappingConfiguration> GetMappingConfigs()
        {
            foreach (
                var type in
                _assemblyService.GetPluginAssemblies()
                    .GetTypes()
                    .Where(t => !t.IsAbstract && typeof(IMappingConfiguration).IsAssignableFrom(t)))
            {
                yield return Instantiate(type);
            }
        }

        private IMappingConfiguration Instantiate(Type type)
        {
            return (IMappingConfiguration)_instantiationService.GetInstance(type);
        }

        #endregion

        #region Exposed Methods

        public void Configure(MappingConfiguration mappingConfiguration)
        {
            foreach (var mappingConfig in GetMappingConfigs())
            {
                mappingConfig.Configure(mappingConfiguration);
            }
        }

        #endregion
    }
}