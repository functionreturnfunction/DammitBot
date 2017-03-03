using System.Linq;

namespace DammitBot.Data.Library
{
    /// <summary>
    /// Main interface for database access.  Shoud be used in conjunction with an IUnitOfWork.
    /// </summary>
    /// <typeparam name="T">Type of entity the repository deals with.</typeparam>
    public interface IRepository<T> : IQueryable<T>
    {
        T Save(T entity);
        T Find(object id);
    }
}