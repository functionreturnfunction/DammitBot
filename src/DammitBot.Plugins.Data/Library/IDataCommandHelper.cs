using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public interface IDataCommandHelper
    {
        void Save(object entity);
        T Load<T>(object id);
        IQueryable<T> Where<T>(Expression<Func<T, bool>> fn);
    }
}