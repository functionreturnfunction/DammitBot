using System;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
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
}