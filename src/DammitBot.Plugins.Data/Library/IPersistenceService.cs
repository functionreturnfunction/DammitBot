using System;
using System.Linq;

namespace DammitBot.Data.Library
{
    /// <summary>
    /// Service for simple CRUD access to data without having to consume repositories or units of work.
    /// This is most likely a crutch if you are making more than one call with it.
    /// </summary>
    public interface IPersistenceService : IDisposable
    {
        object Insert<T>(T obj) where T : class;
        void Update<T>(T obj) where T : class;
        T Find<T>(int id) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
