namespace DammitBot.Utilities
{
    public interface IMigrationService
    {
        #region Abstract Methods

        void EnsureUpToDate();

        #endregion
    }
}