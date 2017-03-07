using System;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;

namespace DammitBot.Utilities
{
    public class MigrationProcessorOptions : IMigrationProcessorOptions
    {
        #region Constants

        public struct Defaults
        {
            #region Constants

            public const bool PREVIEW_ONLY = false;
            public const int TIMEOUT = 60;

            #endregion
        }

        #endregion

        #region Properties

        public bool PreviewOnly { get; set; } = Defaults.PREVIEW_ONLY;
        public int Timeout { get; set; } = Defaults.TIMEOUT;
        public string ProviderSwitches { get; set; }
        public Type AnnouncerType { get; set; } = typeof(NullAnnouncer);
        public Type FactoryType { get; }

        #endregion

        #region Constructors

        public MigrationProcessorOptions(Type factoryType)
        {
            FactoryType = factoryType;
        }

        #endregion
    }

    public class MigrationProcessorOptions<TAnnouncer, TFactory> : MigrationProcessorOptions
        where TAnnouncer : IAnnouncer
        where TFactory : IMigrationProcessorFactory
    {
        #region Constructors

        public MigrationProcessorOptions() : base(typeof(TFactory))
        {
            AnnouncerType = typeof(TAnnouncer);
        }

        #endregion
    }

    public class MigrationRunner : FluentMigrator.Runner.MigrationRunner
    {
        public MigrationRunner(IAssemblyCollection assemblies, IRunnerContext runnerContext, IMigrationProcessor processor) : base(assemblies, runnerContext, processor) {}
    }
}