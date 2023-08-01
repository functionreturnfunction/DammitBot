namespace DammitBot.Library
{
    /// <summary>
    /// Builds IUnitOfWork objects.  If you plan to do data access with repositories you should consume one of these.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Build();
    }
}