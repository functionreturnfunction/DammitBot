using System.Linq;

namespace DammitBot.Library
{
    /// <summary>
    /// Main interface for database access.  Shoud be used in conjunction with an IUnitOfWork.
    /// </summary>
    /// <typeparam name="T">Type of entity the repository deals with.</typeparam>
    public interface IRepository<T> : IQueryable<T>
        where T : class
    {
        object Insert(T entity);
        void Update(T entity);
        T Find(int id);
    }
}
