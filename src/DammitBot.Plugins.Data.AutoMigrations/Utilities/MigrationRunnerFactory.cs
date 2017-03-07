using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using StructureMap;

namespace DammitBot.Utilities
{
    public class MigrationRunnerFactory : IMigrationRunnerFactory
    {
        #region Private Members

        private readonly IMigrationProcessorService _processorService;
        private readonly IContainer _container;

        #endregion

        #region Constructors

        public MigrationRunnerFactory(IMigrationProcessorService processorService, IContainer container)
        {
            _processorService = processorService;
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public IMigrationRunner Build(IAssemblyCollection migrationAssemblies, IMigrationProcessorOptions options)
        {
            var processor = _processorService.GetProcessor(options);
            var announcer = (IAnnouncer)_container.GetInstance(options.AnnouncerType);
            var context = _container
                .With("announcer")
                .EqualTo(announcer)
                .GetInstance<IRunnerContext>();
            return _container
                .With("assemblies")
                .EqualTo(migrationAssemblies)
                .With("runnerContext")
                .EqualTo(context)
                .With("processor")
                .EqualTo(processor)
                .GetInstance<IMigrationRunner>();
        }

        #endregion
    }
}