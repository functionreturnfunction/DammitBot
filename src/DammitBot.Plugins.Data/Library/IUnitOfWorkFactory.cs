namespace DammitBot.Data.Library
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Build();
    }
}