namespace DammitBot.Utilities
{
    public interface IMigrationService
    {
        #region Abstract Methods

        void EnsureUpToDate(IMigrationProcessorOptions options);
        long? LatestVersionNumber { get; }

        #endregion
    }
}