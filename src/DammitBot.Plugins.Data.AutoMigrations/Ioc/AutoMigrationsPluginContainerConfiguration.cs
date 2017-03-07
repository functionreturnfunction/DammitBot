using DammitBot.Abstract;
using DammitBot.Utilities;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using StructureMap;
using MigrationRunner = DammitBot.Utilities.MigrationRunner;

namespace DammitBot.Ioc
{
    public class AutoMigrationsPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<IFlushableLogAnnouncer>().Use<FlushableLogAnnouncer>().Singleton();
            e.For<IRunnerContext>().Use<RunnerContext>();
            e.For<IMigrationRunner>().Use<MigrationRunner>();
        }

        #endregion
    }
}
