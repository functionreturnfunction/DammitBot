namespace DammitBot.Data.Library
{
    /// <summary>
    /// Class which handles database session and transaction details.  If you're working with a repository you need one
    /// of these, and if you don't Commit() and Dispose() it your changes will not be saved.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Abstract Methods

        IDisposableUnitOfWork Start();

        #endregion
    }
}