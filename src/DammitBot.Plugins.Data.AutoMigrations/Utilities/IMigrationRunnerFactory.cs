using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;

namespace DammitBot.Utilities
{
    public interface IMigrationRunnerFactory
    {
        #region Abstract Methods

        IMigrationRunner Build(IAssemblyCollection migrationAssemblies, IMigrationProcessorOptions options);

        #endregion
    }
}