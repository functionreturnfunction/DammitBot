using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public interface IRepository<T>
    {
        T Save(T entity);
        T Find(object id);
        IQueryable<T> Where(Expression<Func<T, bool>> fn);
    }
}