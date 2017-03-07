using DammitBot.Configuration;
using DammitBot.Wrappers;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;

namespace DammitBot.Utilities
{
    public class MigrationProcessorService : IMigrationProcessorService
    {
        #region Private Members

        private readonly IInstantiationService _instantiationService;
        private readonly IDataConfigurationManager _dataConfiguration;

        #endregion

        #region Constructors

        public MigrationProcessorService(IInstantiationService instantiationService, IDataConfigurationManager dataConfiguration)
        {
            _instantiationService = instantiationService;
            _dataConfiguration = dataConfiguration;
        }

        #endregion

        #region Exposed Methods

        public IMigrationProcessor GetProcessor(IMigrationProcessorOptions options)
        {
            var announcer = (IAnnouncer)_instantiationService.GetInstance(options.AnnouncerType);
            var factory = (IMigrationProcessorFactory)_instantiationService.GetInstance(options.FactoryType);
            return factory.Create(_dataConfiguration.ConnectionString, announcer, options);
        }

        #endregion
    }
}