using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        public abstract TEntity Save(TEntity entity);
        public abstract TEntity Find(object id);
        public abstract IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> fn);
    }
}