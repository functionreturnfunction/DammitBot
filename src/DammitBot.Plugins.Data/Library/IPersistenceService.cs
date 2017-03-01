using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public interface IPersistenceService : IDisposable
    {
        void Save<T>(T obj);
        T Find<T>(object id);
        IQueryable<T> Where<T>(Expression<Func<T, bool>> fn);
    }
}