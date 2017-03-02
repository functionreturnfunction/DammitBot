using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    /// <summary>
    /// Service for simple CRUD access to data without having to consume repositories or units of work.
    /// This is most likely a crutch if you are making more than one call with it.
    /// </summary>
    public interface IPersistenceService : IDisposable
    {
        void Save<T>(T obj);
        T Find<T>(object id);
        IQueryable<T> Where<T>(Expression<Func<T, bool>> fn);
    }
}