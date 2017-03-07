using FluentMigrator;

namespace DammitBot.Utilities
{
    public interface IMigrationProcessorService
    {
        #region Abstract Methods

        IMigrationProcessor GetProcessor(IMigrationProcessorOptions options);

        #endregion
    }
}